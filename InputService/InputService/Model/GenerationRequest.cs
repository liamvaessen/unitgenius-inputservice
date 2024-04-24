using System;
using System.Diagnostics.CodeAnalysis;

namespace InputService.Model
{
    /// <summary>
    /// Represents a generation request.
    /// </summary>
    public class GenerationRequest
    {
        /// <summary>
        /// Gets or sets the unique identifier for the request.
        /// </summary>
        public Guid RequestId { get; set; }
        
        /// <summary>
        /// Gets or sets the unique identifier for the user making the request.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the type of generation (Unit Test or Documentation).
        /// </summary>
        public GenerationType GenerationType { get; set; }

        /// <summary>
        /// Gets or sets the status of the generation request.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Gets or sets the generated code.
        /// </summary>
        [AllowNull]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the parameter for the generation request.
        /// </summary>
        [AllowNull]
        public string? Parameter { get; set; }

        /// <summary>
        /// Gets or sets the result of the generation request.
        /// </summary>
        [AllowNull]
        public string? Result { get; set; }
    }

    /// <summary>
    /// Represents the type of generation.
    /// </summary>
    public enum GenerationType
    {
        UnitTest,
        Documentation
    }

    /// <summary>
    /// Represents the status of a generation request.
    /// </summary>
    public enum Status
    {
        Requested,
        Completed,
        Failed
    }
}
