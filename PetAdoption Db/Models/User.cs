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
        [Required(ErrorMessage = "Please enter name"), MaxLength(50)]
        [DataType(DataType.Text)]
        [Display(Name = "Username")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please enter your EmailAddress")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
    }
}