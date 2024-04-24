using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InputService.Model
{
    /// <summary>
    /// Represents the response body of an input HTTP request.
    /// </summary>
    public class InputHttpResponseBody
    {
        /// <summary>
        /// Gets or sets the unique identifier for the request.
        /// </summary>
        public Guid RequestId { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the generation type of the input.
        /// Will be converted to GenerationType enum: 0 = UnitTest, 1 = Documentation.
        /// </summary>
        public int GenerationType { get; set; }

        /// <summary>
        /// Gets or sets the status of the input.
        /// Will be converted to Status enum: 0 = Requested, 1 = Completed, 2 = Failed.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the code associated with the input.
        /// </summary>
        [AllowNull]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the parameter associated with the input.
        /// </summary>
        [AllowNull]
        public string? Parameter { get; set; }
    }
}
