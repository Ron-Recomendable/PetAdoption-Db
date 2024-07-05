using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace PetAdoption_Db.Models
{
   
    
    public class User
    {
        [Required]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter name"), MaxLength(50)] //Ensures that the 'Username' cannot be null or empty and provides an error message if the validation fails and limits the
                                                                      //length of the Username characters to 50 characters
                                                                        
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your EmailAddress")] //Ensures that the 'Email' is not null or emmpty
        [DataType(DataType.EmailAddress)] //Specifies data type to ensure that the user inputs a valid email address.
        public string Email { get; set; }
        [Required] //This is also requiredS
        public string Role { get; set; }
    }
}
