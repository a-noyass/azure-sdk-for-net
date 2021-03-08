// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Azure.AI.TextAnalytics.Models;

namespace Azure.AI.TextAnalytics
{
    internal static class Transforms
    {
        #region Common

        internal static TextAnalyticsError ConvertToError(TextAnalyticsErrorInternal error)
        {
            string errorCode = error.Code;
            string message = error.Message;
            string target = error.Target;
            InnerError innerError = error.Innererror;

            if (innerError != null)
            {
                // Return the innermost error, which should be only one level down.
                return new TextAnalyticsError(innerError.Code, innerError.Message, innerError.Target);
            }

            return new TextAnalyticsError(errorCode, message, target);
        }

        internal static List<TextAnalyticsError> ConvertToErrors(IReadOnlyList<TextAnalyticsErrorInternal> internalErrors)
        {
            var errors = new List<TextAnalyticsError>();

            if (internalErrors == null)
            {
                return errors;
            }

            foreach (TextAnalyticsErrorInternal error in internalErrors)
            {
                errors.Add(ConvertToError(error));
            }
            return errors;
        }

        internal static List<TextAnalyticsWarning> ConvertToWarnings(IReadOnlyList<TextAnalyticsWarningInternal> internalWarnings)
        {
            var warnings = new List<TextAnalyticsWarning>();

            if (internalWarnings == null)
            {
                return warnings;
            }

            foreach (TextAnalyticsWarningInternal warning in internalWarnings)
            {
                warnings.Add(new TextAnalyticsWarning(warning));
            }
            return warnings;
        }

        #endregion

        #region DetectLanguage

        internal static DetectedLanguage ConvertToDetectedLanguage(DocumentLanguage documentLanguage)
        {
            return new DetectedLanguage(documentLanguage.DetectedLanguage, ConvertToWarnings(documentLanguage.Warnings));
        }

        internal static DetectLanguageResultCollection ConvertToDetectLanguageResultCollection(LanguageResult results, IDictionary<string, int> idToIndexMap)
        {
            var detectedLanguages = new List<DetectLanguageResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                detectedLanguages.Add(new DetectLanguageResult(error.Id, ConvertToError(error.Error)));
            }

            //Read languages
            foreach (DocumentLanguage language in results.Documents)
            {
                detectedLanguages.Add(new DetectLanguageResult(language.Id, language.Statistics ?? default, ConvertToDetectedLanguage(language)));
            }

            detectedLanguages = SortHeterogeneousCollection(detectedLanguages, idToIndexMap);

            return new DetectLanguageResultCollection(detectedLanguages, results.Statistics, results.ModelVersion);
        }

        #endregion

        #region AnalyzeSentiment

        internal static AnalyzeSentimentResultCollection ConvertToAnalyzeSentimentResultCollection(SentimentResponse results, IDictionary<string, int> idToIndexMap)
        {
            var analyzedSentiments = new List<AnalyzeSentimentResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                analyzedSentiments.Add(new AnalyzeSentimentResult(error.Id, ConvertToError(error.Error)));
            }

            //Read sentiments
            foreach (DocumentSentimentInternal docSentiment in results.Documents)
            {
                analyzedSentiments.Add(new AnalyzeSentimentResult(docSentiment.Id, docSentiment.Statistics ?? default, new DocumentSentiment(docSentiment)));
            }

            analyzedSentiments = SortHeterogeneousCollection(analyzedSentiments, idToIndexMap);

            return new AnalyzeSentimentResultCollection(analyzedSentiments, results.Statistics, results.ModelVersion);
        }

        #endregion

        #region KeyPhrases

        internal static KeyPhraseCollection ConvertToKeyPhraseCollection(DocumentKeyPhrases documentKeyPhrases)
        {
            return new KeyPhraseCollection(documentKeyPhrases.KeyPhrases.ToList(), ConvertToWarnings(documentKeyPhrases.Warnings));
        }

        internal static ExtractKeyPhrasesResultCollection ConvertToExtractKeyPhrasesResultCollection(KeyPhraseResult results, IDictionary<string, int> idToIndexMap)
        {
            var keyPhrases = new List<ExtractKeyPhrasesResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                keyPhrases.Add(new ExtractKeyPhrasesResult(error.Id, ConvertToError(error.Error)));
            }

            //Read Key phrases
            foreach (DocumentKeyPhrases docKeyPhrases in results.Documents)
            {
                keyPhrases.Add(new ExtractKeyPhrasesResult(docKeyPhrases.Id, docKeyPhrases.Statistics ?? default, ConvertToKeyPhraseCollection(docKeyPhrases)));
            }

            keyPhrases = SortHeterogeneousCollection(keyPhrases, idToIndexMap);

            return new ExtractKeyPhrasesResultCollection(keyPhrases, results.Statistics, results.ModelVersion);
        }

        #endregion

        #region Recognize Entities

        internal static List<CategorizedEntity> ConvertToCategorizedEntityList(List<Entity> entities)
            => entities.Select((entity) => new CategorizedEntity(entity)).ToList();

        internal static CategorizedEntityCollection ConvertToCategorizedEntityCollection(DocumentEntities documentEntities)
        {
            return new CategorizedEntityCollection(ConvertToCategorizedEntityList(documentEntities.Entities.ToList()), ConvertToWarnings(documentEntities.Warnings));
        }

        internal static RecognizeEntitiesResultCollection ConvertToRecognizeEntitiesResultCollection(EntitiesResult results, IDictionary<string, int> idToIndexMap)
        {
            var recognizeEntities = new List<RecognizeEntitiesResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                recognizeEntities.Add(new RecognizeEntitiesResult(error.Id, ConvertToError(error.Error)));
            }

            //Read document entities
            foreach (DocumentEntities docEntities in results.Documents)
            {
                recognizeEntities.Add(new RecognizeEntitiesResult(docEntities.Id, docEntities.Statistics ?? default, ConvertToCategorizedEntityCollection(docEntities)));
            }

            recognizeEntities = SortHeterogeneousCollection(recognizeEntities, idToIndexMap);

            return new RecognizeEntitiesResultCollection(recognizeEntities, results.Statistics, results.ModelVersion);
        }

        #endregion

        #region Recognize PII Entities

        internal static List<PiiEntity> ConvertToPiiEntityList(List<Entity> entities)
            => entities.Select((entity) => new PiiEntity(entity)).ToList();

        internal static PiiEntityCollection ConvertToPiiEntityCollection(PiiDocumentEntities documentEntities)
        {
            return new PiiEntityCollection(ConvertToPiiEntityList(documentEntities.Entities.ToList()), documentEntities.RedactedText, ConvertToWarnings(documentEntities.Warnings));
        }

        internal static RecognizePiiEntitiesResultCollection ConvertToRecognizePiiEntitiesResultCollection(PiiEntitiesResult results, IDictionary<string, int> idToIndexMap)
        {
            var recognizeEntities = new List<RecognizePiiEntitiesResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                recognizeEntities.Add(new RecognizePiiEntitiesResult(error.Id, ConvertToError(error.Error)));
            }

            //Read document entities
            foreach (PiiDocumentEntities docEntities in results.Documents)
            {
                recognizeEntities.Add(new RecognizePiiEntitiesResult(docEntities.Id, docEntities.Statistics ?? default, ConvertToPiiEntityCollection(docEntities)));
            }

            recognizeEntities = SortHeterogeneousCollection(recognizeEntities, idToIndexMap);

            return new RecognizePiiEntitiesResultCollection(recognizeEntities, results.Statistics, results.ModelVersion);
        }

        #endregion

        #region Recognize Linked Entities

        internal static LinkedEntityCollection ConvertToLinkedEntityCollection(DocumentLinkedEntities documentEntities)
        {
            return new LinkedEntityCollection(documentEntities.Entities.ToList(), ConvertToWarnings(documentEntities.Warnings));
        }

        internal static RecognizeLinkedEntitiesResultCollection ConvertToRecognizeLinkedEntitiesResultCollection(EntityLinkingResult results, IDictionary<string, int> idToIndexMap)
        {
            var recognizeEntities = new List<RecognizeLinkedEntitiesResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                recognizeEntities.Add(new RecognizeLinkedEntitiesResult(error.Id, ConvertToError(error.Error)));
            }

            //Read document linked entities
            foreach (DocumentLinkedEntities docEntities in results.Documents)
            {
                recognizeEntities.Add(new RecognizeLinkedEntitiesResult(docEntities.Id, docEntities.Statistics ?? default, ConvertToLinkedEntityCollection(docEntities)));
            }

            recognizeEntities = SortHeterogeneousCollection(recognizeEntities, idToIndexMap);

            return new RecognizeLinkedEntitiesResultCollection(recognizeEntities, results.Statistics, results.ModelVersion);
        }

        #endregion

        #region Recognize Custom Entities

        internal static List<CustomEntity> ConvertToCustomEntityList(List<Entity> entities)
            => entities.Select((entity) => new CustomEntity(entity)).ToList();

        internal static CustomEntityCollection ConvertToCustomEntityCollection(DocumentCustomEntities documentEntities)
        {
            return new CustomEntityCollection(ConvertToCustomEntityList(documentEntities.Entities.ToList()), ConvertToWarnings(documentEntities.Warnings));
        }

        internal static RecognizeCustomEntitiesResultCollection ConvertToRecognizeCustomEntitiesResultCollection(CustomEntitiesResult results, IDictionary<string, int> idToIndexMap)
        {
            var recognizeEntities = new List<RecognizeCustomEntitiesResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                recognizeEntities.Add(new RecognizeCustomEntitiesResult(error.Id, ConvertToError(error.Error)));
            }

            //Read document custom entities
            foreach (DocumentCustomEntities docEntities in results.Documents)
            {
                recognizeEntities.Add(new RecognizeCustomEntitiesResult(docEntities.Id, docEntities.Statistics ?? default, ConvertToCustomEntityCollection(docEntities)));
            }

            recognizeEntities = SortHeterogeneousCollection(recognizeEntities, idToIndexMap);

            return new RecognizeCustomEntitiesResultCollection(recognizeEntities, results.Statistics);
        }

        #endregion

        #region Custom Classification

        internal static CustomClassificationResultCollection ConvertToCustomClassificationResultCollection(InternalCustomClassificationResult results, IDictionary<string, int> idToIndexMap)
        {
            var customClassifications = new List<CustomClassificationResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                customClassifications.Add(new CustomClassificationResult(error.Id, ConvertToError(error.Error)));
            }

            //Read document custom classification
            foreach (DocumentCustomClassification docClassification in results.Documents)
            {
                customClassifications.Add(new CustomClassificationResult(docClassification.Id, docClassification.Statistics ?? default, new CustomClassification(docClassification.Classification)));
            }

            customClassifications = SortHeterogeneousCollection(customClassifications, idToIndexMap);

            return new CustomClassificationResultCollection(customClassifications, results.Statistics);
        }

        #endregion

        #region Custom Multi Classification

        internal static List<CustomClassification> ConvertToCustomClassificationList(List<Classification> classifications)
            => classifications.Select((classification) => new CustomClassification(classification)).ToList();

        internal static CustomClassificationCollection ConvertToCustomClassificationCollection(DocumentCustomMultiClassification documentEntities)
        {
            return new CustomClassificationCollection(ConvertToCustomClassificationList(documentEntities.Classifications.ToList()), ConvertToWarnings(documentEntities.Warnings));
        }

        internal static CustomMultiClassificationResultCollection ConvertToCustomMultiClassificationResultCollection(InternalCustomMultiClassificationResult results, IDictionary<string, int> idToIndexMap)
        {
            var customMultiClassifications = new List<CustomMultiClassificationResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                customMultiClassifications.Add(new CustomMultiClassificationResult(error.Id, ConvertToError(error.Error)));
            }

            //Read document custom multi classifications
            foreach (DocumentCustomMultiClassification docMultiClassification in results.Documents)
            {
                customMultiClassifications.Add(new CustomMultiClassificationResult(docMultiClassification.Id, docMultiClassification.Statistics ?? default, ConvertToCustomClassificationCollection(docMultiClassification)));
            }

            customMultiClassifications = SortHeterogeneousCollection(customMultiClassifications, idToIndexMap);

            return new CustomMultiClassificationResultCollection(customMultiClassifications, results.Statistics);
        }

        #endregion

        #region Healthcare

        internal static List<HealthcareEntity> ConvertToHealthcareEntityCollection(IEnumerable<HealthcareEntityInternal> healthcareEntities)
        {
            return healthcareEntities.Select((entity) => new HealthcareEntity(entity)).ToList();
        }

        internal static AnalyzeHealthcareEntitiesResultCollection ConvertToAnalyzeHealthcareEntitiesResultCollection(HealthcareResult results, IDictionary<string, int> idToIndexMap)
        {
            var healthcareEntititesResults = new List<AnalyzeHealthcareEntitiesResult>();

            //Read errors
            foreach (DocumentError error in results.Errors)
            {
                healthcareEntititesResults.Add(new AnalyzeHealthcareEntitiesResult(error.Id, ConvertToError(error.Error)));
            }

            //Read entities
            foreach (DocumentHealthcareEntitiesInternal documentHealthcareEntities in results.Documents)
            {
                healthcareEntititesResults.Add(new AnalyzeHealthcareEntitiesResult(
                    documentHealthcareEntities.Id,
                    documentHealthcareEntities.Statistics ?? default,
                    ConvertToHealthcareEntityCollection(documentHealthcareEntities.Entities),
                    ConvertToHealthcareEntityRelationsCollection(documentHealthcareEntities.Entities, documentHealthcareEntities.Relations),
                    ConvertToWarnings(documentHealthcareEntities.Warnings)));
            }

            healthcareEntititesResults = healthcareEntititesResults.OrderBy(result => idToIndexMap[result.Id]).ToList();

            return new AnalyzeHealthcareEntitiesResultCollection(healthcareEntititesResults, results.Statistics, results.ModelVersion);
        }

        private static IList<HealthcareEntityRelation> ConvertToHealthcareEntityRelationsCollection(IReadOnlyList<HealthcareEntityInternal> healthcareEntities, IReadOnlyList<HealthcareRelationInternal> healthcareRelations)
        {
            List<HealthcareEntityRelation> result = new List<HealthcareEntityRelation>();
            foreach (HealthcareRelationInternal relation in healthcareRelations)
            {
                result.Add(new HealthcareEntityRelation(relation.RelationType, ConvertToHealthcareEntityRelationRoleCollection(relation.Entities, healthcareEntities)));
            }
            return result;
        }

        private static IReadOnlyCollection<HealthcareEntityRelationRole> ConvertToHealthcareEntityRelationRoleCollection(IReadOnlyList<HealthcareRelationEntity> entities, IReadOnlyList<HealthcareEntityInternal> healthcareEntities)
        {
            List<HealthcareEntityRelationRole> result = new List<HealthcareEntityRelationRole>();

            foreach (HealthcareRelationEntity entity in entities)
            {
                int refIndex = ParseHealthcareEntityIndex(entity.Ref);
                HealthcareEntityInternal refEntity = healthcareEntities[refIndex];

                result.Add(new HealthcareEntityRelationRole(refEntity, entity.Role));
            }

            return result;
        }

        private static int ParseHealthcareEntityIndex(string reference)
        {
            Match healthcareEntityMatch = _healthcareEntityRegex.Match(reference);
            if (healthcareEntityMatch.Success)
            {
                return int.Parse(healthcareEntityMatch.Groups["entityIndex"].Value, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException($"Failed to parse element reference: {reference}");
        }

        private static Regex _healthcareEntityRegex = new Regex(@"\#/results/documents\/(?<documentIndex>\d*)\/entities\/(?<entityIndex>\d*)$", RegexOptions.Compiled, TimeSpan.FromSeconds(2));

        #endregion

        #region Analyze Operation

        internal static PiiTask ConvertToPiiTask(RecognizePiiEntitiesOptions option)
        {
            return new PiiTask()
            {
                Parameters = new PiiTaskParameters()
                {
                    Domain = option.DomainFilter.HasValue ? option.DomainFilter.Value.GetString() : (PiiTaskParametersDomain?)null,
                    ModelVersion = !string.IsNullOrEmpty(option.ModelVersion) ? option.ModelVersion : "latest",
                    StringIndexType = option.StringIndexType
                }
            };
        }

        internal static EntityLinkingTask ConvertToLinkedEntitiesTask(RecognizeLinkedEntitiesOptions option)
        {
            return new EntityLinkingTask()
            {
                Parameters = new EntityLinkingTaskParameters()
                {
                    ModelVersion = !string.IsNullOrEmpty(option.ModelVersion) ? option.ModelVersion : "latest",
                    StringIndexType = option.StringIndexType
                }
            };
        }

        internal static EntitiesTask ConvertToEntitiesTask(RecognizeEntitiesOptions option)
        {
            return new EntitiesTask()
            {
                Parameters = new EntitiesTaskParameters()
                {
                    ModelVersion = !string.IsNullOrEmpty(option.ModelVersion) ? option.ModelVersion : "latest",
                    StringIndexType = option.StringIndexType
                }
            };
        }

        internal static KeyPhrasesTask ConvertToKeyPhrasesTask(ExtractKeyPhrasesOptions option)
        {
            return new KeyPhrasesTask()
            {
                Parameters = new KeyPhrasesTaskParameters()
                {
                    ModelVersion = !string.IsNullOrEmpty(option.ModelVersion) ? option.ModelVersion : "latest",
                }
            };
        }

        internal static IList<EntityLinkingTask> ConvertFromEntityLinkingOptionsToTasks(IReadOnlyCollection<RecognizeLinkedEntitiesOptions> recognizeLinkedEntityOptions)
        {
            List<EntityLinkingTask> list = new List<EntityLinkingTask>();

            foreach (RecognizeLinkedEntitiesOptions option in recognizeLinkedEntityOptions)
            {
                list.Add(ConvertToLinkedEntitiesTask(option));
            }

            return list;
        }

        internal static IList<EntitiesTask> ConvertFromEntityOptionsToTasks(IReadOnlyCollection<RecognizeEntitiesOptions> recognizeEntitiesOptions)
        {
            List<EntitiesTask> list = new List<EntitiesTask>();

            foreach (RecognizeEntitiesOptions option in recognizeEntitiesOptions)
            {
                list.Add(ConvertToEntitiesTask(option));
            }

            return list;
        }

        internal static CustomEntitiesTask ConvertToCustomEntitiesTask(RecognizeCustomEntitiesOptions option)
        {
            return new CustomEntitiesTask()
            {
                Parameters = new CustomEntitiesTaskParameters()
                {
                    PublishId = option.PublishId,
                    StringIndexType = option.StringIndexType
                }
            };
        }

        internal static CustomClassificationTask ConvertToCustomClassificationTask(CustomClassificationOptions option)
        {
            return new CustomClassificationTask()
            {
                Parameters = new CustomClassificationTaskParameters()
                {
                    PublishId = option.PublishId
                }
            };
        }

        internal static CustomMultiClassificationTask ConvertToCustomMultiClassificationTask(CustomMultiClassificationOptions option)
        {
            return new CustomMultiClassificationTask()
            {
                Parameters = new CustomMultiClassificationTaskParameters()
                {
                    PublishId = option.PublishId
                }
            };
        }

        internal static IList<KeyPhrasesTask> ConvertFromKeyPhrasesOptionsToTasks(IReadOnlyCollection<ExtractKeyPhrasesOptions> extractKeyPhrasesOptions)
        {
            List<KeyPhrasesTask> list = new List<KeyPhrasesTask>();

            foreach (ExtractKeyPhrasesOptions option in extractKeyPhrasesOptions)
            {
                list.Add(ConvertToKeyPhrasesTask(option));
            }

            return list;
        }

        internal static IList<PiiTask> ConvertFromPiiEntityOptionsToTasks(IReadOnlyCollection<RecognizePiiEntitiesOptions> recognizePiiEntityOptions)
        {
            List <PiiTask> list = new List<PiiTask>();

            foreach (RecognizePiiEntitiesOptions option in recognizePiiEntityOptions)
            {
                list.Add(ConvertToPiiTask(option));
            }

            return list;
        }

        internal static IList<CustomEntitiesTask> ConvertFromCustomEntityOptionsToTasks(IReadOnlyCollection<RecognizeCustomEntitiesOptions> recognizeCustomEntitiesOptions)
        {
            List<CustomEntitiesTask> list = new List<CustomEntitiesTask>();

            foreach (RecognizeCustomEntitiesOptions option in recognizeCustomEntitiesOptions)
            {
                list.Add(ConvertToCustomEntitiesTask(option));
            }

            return list;
        }

        internal static IList<CustomClassificationTask> ConvertFromCustomClassificationOptionsToTasks(IReadOnlyCollection<CustomClassificationOptions> customClassificationOptions)
        {
            List<CustomClassificationTask> list = new List<CustomClassificationTask>();

            foreach (CustomClassificationOptions option in customClassificationOptions)
            {
                list.Add(ConvertToCustomClassificationTask(option));
            }

            return list;
        }

        internal static IList<CustomMultiClassificationTask> ConvertFromCustomMultiClassificationOptionsToTasks(IReadOnlyCollection<CustomMultiClassificationOptions> customMultiClassificationOptions)
        {
            List<CustomMultiClassificationTask> list = new List<CustomMultiClassificationTask>();

            foreach (CustomMultiClassificationOptions option in customMultiClassificationOptions)
            {
                list.Add(ConvertToCustomMultiClassificationTask(option));
            }

            return list;
        }

        internal static AnalyzeBatchActionsResult ConvertToAnalyzeOperationResult(AnalyzeJobState jobState, IDictionary<string, int> map)
        {
            return new AnalyzeBatchActionsResult(jobState, map);
        }

        internal static IReadOnlyList<KeyPhraseExtractionTasksItem> ConvertToKeyPhraseExtractionTasks(IReadOnlyList<KeyPhraseExtractionTasksItem> keyPhraseExtractionTasks, IDictionary<string, int> idToIndexMap)
        {
            var collection = new List<KeyPhraseExtractionTasksItem>();
            foreach (KeyPhraseExtractionTasksItem task in keyPhraseExtractionTasks)
            {
                collection.Add(new KeyPhraseExtractionTasksItem(task, idToIndexMap));
            }

            return collection;
        }

        internal static IReadOnlyList<EntityRecognitionPiiTasksItem> ConvertToEntityRecognitionPiiTasks(IReadOnlyList<EntityRecognitionPiiTasksItem> entityRecognitionPiiTasks, IDictionary<string, int> idToIndexMap)
        {
            var collection = new List<EntityRecognitionPiiTasksItem>();
            foreach (EntityRecognitionPiiTasksItem task in entityRecognitionPiiTasks)
            {
                collection.Add(new EntityRecognitionPiiTasksItem(task, idToIndexMap));
            }

            return collection;
        }

        internal static IReadOnlyList<EntityRecognitionTasksItem> ConvertToEntityRecognitionTasks(IReadOnlyList<EntityRecognitionTasksItem> entityRecognitionTasks, IDictionary<string, int> idToIndexMap)
        {
            var collection = new List<EntityRecognitionTasksItem>();
            foreach (EntityRecognitionTasksItem task in entityRecognitionTasks)
            {
                collection.Add(new EntityRecognitionTasksItem(task, idToIndexMap));
            }

            return collection;
        }

        private static string[] parseActionErrorTarget(string targetReference)
        {
            if (string.IsNullOrEmpty(targetReference))
            {
                throw new InvalidOperationException("Expected an error with a target field referencing an action but did not get one");
            }
            Regex _targetRegex = new Regex("#/tasks/(keyPhraseExtractionTasks|entityRecognitionPiiTasks|entityRecognitionTasks|entityLinkingTasks)/(\\d+)", RegexOptions.Compiled, TimeSpan.FromSeconds(2));

            // action could be failed and the target reference is "#/tasks/keyPhraseExtractionTasks/0";
            Match targetMatch = _targetRegex.Match(targetReference);

            string[] taskNameIdPair = new string[2];
            if (targetMatch.Success && targetMatch.Groups.Count == 3)
            {
                taskNameIdPair[0] = targetMatch.Groups[1].Value;
                taskNameIdPair[1] = targetMatch.Groups[2].Value;
            }
            return taskNameIdPair;
        }

        internal static AnalyzeBatchActionsResult ConvertToAnalyzeBatchActionsResult(AnalyzeJobState jobState, IDictionary<string, int> map)
        {
            IDictionary<int, TextAnalyticsErrorInternal> keyPhraseErrors = new Dictionary<int, TextAnalyticsErrorInternal>();
            IDictionary<int, TextAnalyticsErrorInternal> entitiesRecognitionErrors = new Dictionary<int, TextAnalyticsErrorInternal>();
            IDictionary<int, TextAnalyticsErrorInternal> entitiesPiiRecognitionErrors = new Dictionary<int, TextAnalyticsErrorInternal>();
            IDictionary<int, TextAnalyticsErrorInternal> entitiesLinkingRecognitionErrors = new Dictionary<int, TextAnalyticsErrorInternal>();
            IDictionary<int, TextAnalyticsErrorInternal> customEntitiesRecognitionErrors = new Dictionary<int, TextAnalyticsErrorInternal>();
            IDictionary<int, TextAnalyticsErrorInternal> customClassificationErrors = new Dictionary<int, TextAnalyticsErrorInternal>();
            IDictionary<int, TextAnalyticsErrorInternal> customMultiClassificationErrors = new Dictionary<int, TextAnalyticsErrorInternal>();

            if (jobState.Errors.Any())
            {
                foreach (TextAnalyticsErrorInternal error in jobState.Errors)
                {
                    string[] targetPair = parseActionErrorTarget(error.Target);
                    string taskName = targetPair[0];
                    int taskIndex = int.Parse(targetPair[1], CultureInfo.InvariantCulture);

                    if ("entityRecognitionTasks".Equals(taskName))
                    {
                        entitiesRecognitionErrors.Add(taskIndex, error);
                    }
                    else if ("entityRecognitionPiiTasks".Equals(taskName))
                    {
                        entitiesPiiRecognitionErrors.Add(taskIndex, error);
                    }
                    else if ("keyPhraseExtractionTasks".Equals(taskName))
                    {
                        keyPhraseErrors.Add(taskIndex, error);
                    }
                    else if ("entityLinkingTasks".Equals(taskName))
                    {
                        entitiesLinkingRecognitionErrors.Add(taskIndex, error);
                    }
                    else if ("customEntityRecognitionTasks".Equals(taskName))
                    {
                        customEntitiesRecognitionErrors.Add(taskIndex, error);
                    }
                    else if ("customClassificationTasks".Equals(taskName))
                    {
                        customClassificationErrors.Add(taskIndex, error);
                    }
                    else if ("customMultiClassificationTasks".Equals(taskName))
                    {
                        customMultiClassificationErrors.Add(taskIndex, error);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Invalid task name in target reference - {taskName}");
                    }
                }
            }

            var extractKeyPhrasesActionResult = ConvertToExtractKeyPhrasesActionResults(jobState, map, keyPhraseErrors);
            var recognizeEntitiesActionResults = ConvertToRecognizeEntitiesActionsResults(jobState, map, entitiesRecognitionErrors);
            var recognizePiiEntitiesActionResults = ConvertToRecognizePiiEntitiesActionsResults(jobState, map, entitiesPiiRecognitionErrors);
            var recognizeLinkedEntitiesActionsResults = ConvertToRecognizeLinkedEntitiesActionsResults(jobState, map, entitiesLinkingRecognitionErrors);
            var recognizeCustomEntitiesActionResults = ConvertToRecognizeCustomEntitiesActionsResults(jobState, map, customEntitiesRecognitionErrors);
            var customClassificationActionResults = ConvertToCustomClassificationActionsResults(jobState, map, customClassificationErrors);
            var customMultiClassificationActionResults = ConvertToCustomMultiClassificationActionsResults(jobState, map, customMultiClassificationErrors);

            return new AnalyzeBatchActionsResult(extractKeyPhrasesActionResult, recognizeEntitiesActionResults, recognizePiiEntitiesActionResults, recognizeLinkedEntitiesActionsResults, recognizeCustomEntitiesActionResults, customClassificationActionResults, customMultiClassificationActionResults, jobState.Statistics);
        }

        private static IReadOnlyCollection<RecognizeLinkedEntitiesActionResult> ConvertToRecognizeLinkedEntitiesActionsResults(AnalyzeJobState jobState, IDictionary<string, int> idToIndexMap, IDictionary<int, TextAnalyticsErrorInternal> tasksErrors)
        {
            var collection = new List<RecognizeLinkedEntitiesActionResult>();
            int index = 0;
            foreach (EntityLinkingTasksItem task in jobState.Tasks.EntityLinkingTasks)
            {
                tasksErrors.TryGetValue(index, out TextAnalyticsErrorInternal taskError);

                if (taskError != null)
                {
                    collection.Add(new RecognizeLinkedEntitiesActionResult(null, task.LastUpdateDateTime, taskError));
                }
                else
                {
                    collection.Add(new RecognizeLinkedEntitiesActionResult(ConvertToRecognizeLinkedEntitiesResultCollection(task.ResultsInternal, idToIndexMap), task.LastUpdateDateTime, null));
                }
                index++;
            }

            return collection;
        }

        internal static IReadOnlyCollection<ExtractKeyPhrasesActionResult> ConvertToExtractKeyPhrasesActionResults(AnalyzeJobState jobState, IDictionary<string, int> idToIndexMap, IDictionary<int, TextAnalyticsErrorInternal> tasksErrors)
        {
            var collection = new List<ExtractKeyPhrasesActionResult>();
            int index = 0;
            foreach (KeyPhraseExtractionTasksItem task in jobState.Tasks.KeyPhraseExtractionTasks)
            {
                tasksErrors.TryGetValue(index, out TextAnalyticsErrorInternal taskError);

                if (taskError != null)
                {
                    collection.Add(new ExtractKeyPhrasesActionResult(null, task.LastUpdateDateTime, taskError));
                }
                else
                {
                    collection.Add(new ExtractKeyPhrasesActionResult(ConvertToExtractKeyPhrasesResultCollection(task.ResultsInternal, idToIndexMap), task.LastUpdateDateTime, null));
                }
                index++;
            }

            return collection;
        }

        internal static IReadOnlyCollection<RecognizePiiEntitiesActionResult> ConvertToRecognizePiiEntitiesActionsResults(AnalyzeJobState jobState, IDictionary<string, int> idToIndexMap, IDictionary<int, TextAnalyticsErrorInternal> tasksErrors)
        {
            var collection = new List<RecognizePiiEntitiesActionResult>();
            int index = 0;
            foreach (EntityRecognitionPiiTasksItem task in jobState.Tasks.EntityRecognitionPiiTasks)
            {
                tasksErrors.TryGetValue(index, out TextAnalyticsErrorInternal taskError);

                if (taskError != null)
                {
                    collection.Add(new RecognizePiiEntitiesActionResult(null, task.LastUpdateDateTime, taskError));
                }
                else
                {
                    collection.Add(new RecognizePiiEntitiesActionResult(ConvertToRecognizePiiEntitiesResultCollection(task.ResultsInternal, idToIndexMap), task.LastUpdateDateTime, taskError));
                }
                index++;
            }

            return collection;
        }

        internal static IReadOnlyCollection<RecognizeEntitiesActionResult> ConvertToRecognizeEntitiesActionsResults(AnalyzeJobState jobState, IDictionary<string, int> idToIndexMap, IDictionary<int, TextAnalyticsErrorInternal> tasksErrors)
        {
            var collection = new List<RecognizeEntitiesActionResult>();
            int index = 0;
            foreach (EntityRecognitionTasksItem task in jobState.Tasks.EntityRecognitionTasks)
            {
                tasksErrors.TryGetValue(index, out TextAnalyticsErrorInternal taskError);

                tasksErrors.TryGetValue(index, out taskError);

                if (taskError != null)
                {
                    collection.Add(new RecognizeEntitiesActionResult(null, task.LastUpdateDateTime, taskError));
                }
                else
                {
                    collection.Add(new RecognizeEntitiesActionResult(ConvertToRecognizeEntitiesResultCollection(task.ResultsInternal, idToIndexMap), task.LastUpdateDateTime, taskError));
                }
                index++;
            }

            return collection;
        }

        internal static IReadOnlyCollection<RecognizeCustomEntitiesActionResult> ConvertToRecognizeCustomEntitiesActionsResults(AnalyzeJobState jobState, IDictionary<string, int> idToIndexMap, IDictionary<int, TextAnalyticsErrorInternal> tasksErrors)
        {
            var collection = new List<RecognizeCustomEntitiesActionResult>();
            int index = 0;
            foreach (CustomEntityRecognitionTasksItem task in jobState.Tasks.CustomEntityRecognitionTasks)
            {
                tasksErrors.TryGetValue(index, out TextAnalyticsErrorInternal taskError);

                tasksErrors.TryGetValue(index, out taskError);

                if (taskError != null)
                {
                    collection.Add(new RecognizeCustomEntitiesActionResult(null, task.LastUpdateDateTime, taskError));
                }
                else
                {
                    collection.Add(new RecognizeCustomEntitiesActionResult(ConvertToRecognizeCustomEntitiesResultCollection(task.ResultsInternal, idToIndexMap), task.LastUpdateDateTime, taskError));
                }
                index++;
            }

            return collection;
        }

        internal static IReadOnlyCollection<CustomClassificationActionResult> ConvertToCustomClassificationActionsResults(AnalyzeJobState jobState, IDictionary<string, int> idToIndexMap, IDictionary<int, TextAnalyticsErrorInternal> tasksErrors)
        {
            var collection = new List<CustomClassificationActionResult>();
            int index = 0;
            foreach (CustomClassificationTasksItem task in jobState.Tasks.CustomClassificationTasks)
            {
                tasksErrors.TryGetValue(index, out TextAnalyticsErrorInternal taskError);

                tasksErrors.TryGetValue(index, out taskError);

                if (taskError != null)
                {
                    collection.Add(new CustomClassificationActionResult(null, task.LastUpdateDateTime, taskError));
                }
                else
                {
                    collection.Add(new CustomClassificationActionResult(ConvertToCustomClassificationResultCollection(task.ResultsInternal, idToIndexMap), task.LastUpdateDateTime, taskError));
                }
                index++;
            }

            return collection;
        }

        internal static IReadOnlyCollection<CustomMultiClassificationActionResult> ConvertToCustomMultiClassificationActionsResults(AnalyzeJobState jobState, IDictionary<string, int> idToIndexMap, IDictionary<int, TextAnalyticsErrorInternal> tasksErrors)
        {
            var collection = new List<CustomMultiClassificationActionResult>();
            int index = 0;
            foreach (CustomMultiClassificationTasksItem task in jobState.Tasks.CustomMultiClassificationTasks)
            {
                tasksErrors.TryGetValue(index, out TextAnalyticsErrorInternal taskError);

                tasksErrors.TryGetValue(index, out taskError);

                if (taskError != null)
                {
                    collection.Add(new CustomMultiClassificationActionResult(null, task.LastUpdateDateTime, taskError));
                }
                else
                {
                    collection.Add(new CustomMultiClassificationActionResult(ConvertToCustomMultiClassificationResultCollection(task.ResultsInternal, idToIndexMap), task.LastUpdateDateTime, taskError));
                }
                index++;
            }

            return collection;
        }

        #endregion

        private static List<T> SortHeterogeneousCollection<T>(List<T> collection, IDictionary<string, int> idToIndexMap) where T : TextAnalyticsResult
        {
            return collection.OrderBy(result => idToIndexMap[result.Id]).ToList();
        }
    }
}
