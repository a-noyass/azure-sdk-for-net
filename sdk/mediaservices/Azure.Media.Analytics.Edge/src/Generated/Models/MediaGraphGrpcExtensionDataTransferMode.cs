// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System;
using System.ComponentModel;

namespace Azure.Media.Analytics.Edge.Models
{
    /// <summary> How frame data should be transmitted to the inference engine. </summary>
    public readonly partial struct MediaGraphGrpcExtensionDataTransferMode : IEquatable<MediaGraphGrpcExtensionDataTransferMode>
    {
        private readonly string _value;

        /// <summary> Determines if two <see cref="MediaGraphGrpcExtensionDataTransferMode"/> values are the same. </summary>
        /// <exception cref="ArgumentNullException"> <paramref name="value"/> is null. </exception>
        public MediaGraphGrpcExtensionDataTransferMode(string value)
        {
            _value = value ?? throw new ArgumentNullException(nameof(value));
        }

        private const string EmbeddedValue = "Embedded";
        private const string SharedMemoryValue = "SharedMemory";

        /// <summary> Frames are transferred embedded into the gRPC messages. </summary>
        public static MediaGraphGrpcExtensionDataTransferMode Embedded { get; } = new MediaGraphGrpcExtensionDataTransferMode(EmbeddedValue);
        /// <summary> Frames are transferred through shared memory. </summary>
        public static MediaGraphGrpcExtensionDataTransferMode SharedMemory { get; } = new MediaGraphGrpcExtensionDataTransferMode(SharedMemoryValue);
        /// <summary> Determines if two <see cref="MediaGraphGrpcExtensionDataTransferMode"/> values are the same. </summary>
        public static bool operator ==(MediaGraphGrpcExtensionDataTransferMode left, MediaGraphGrpcExtensionDataTransferMode right) => left.Equals(right);
        /// <summary> Determines if two <see cref="MediaGraphGrpcExtensionDataTransferMode"/> values are not the same. </summary>
        public static bool operator !=(MediaGraphGrpcExtensionDataTransferMode left, MediaGraphGrpcExtensionDataTransferMode right) => !left.Equals(right);
        /// <summary> Converts a string to a <see cref="MediaGraphGrpcExtensionDataTransferMode"/>. </summary>
        public static implicit operator MediaGraphGrpcExtensionDataTransferMode(string value) => new MediaGraphGrpcExtensionDataTransferMode(value);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj) => obj is MediaGraphGrpcExtensionDataTransferMode other && Equals(other);
        /// <inheritdoc />
        public bool Equals(MediaGraphGrpcExtensionDataTransferMode other) => string.Equals(_value, other._value, StringComparison.InvariantCultureIgnoreCase);

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode() => _value?.GetHashCode() ?? 0;
        /// <inheritdoc />
        public override string ToString() => _value;
    }
}
