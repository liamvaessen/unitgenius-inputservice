using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InputService.Model
{
    /// <summary>
    /// Represents the request body for an input HTTP request.
    /// </summary>
    public class InputHttpRequestBody
    {
        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the generation type.
        /// Will be converted to GenerationType enum: 0 = UnitTest, 1 = Documentation.
        /// </summary>
        public int GenerationType { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [AllowNull]
        public string? Code { get; set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        [AllowNull]
        public string? Parameter { get; set; }
    }
}
