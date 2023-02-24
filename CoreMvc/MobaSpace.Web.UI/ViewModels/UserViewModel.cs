using Microsoft.AspNetCore.Identity;
using MobaSpace.Core.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobaSpace.Web.UI.ViewModels
{
    public class UserRoleViewModel
    {
        public User User {get; set; }
        public List<Role> UserRoles { get; set; } = new List<Role>(Roles.GetRoles().Select(r => new Role() { Nom = r }));

        public UserRoleViewModel()
        {

        }
        public UserRoleViewModel(User user, UserManager<User> userManager)
        {
            User = user;
            UserRoles.ForEach(ur => ur.Selectionne = userManager.IsInRoleAsync(user, ur.Nom).Result);
        }
    }

    public class Role
    {
        public string Nom { get; set; }
        public bool Selectionne { get; set; }
    }
}
