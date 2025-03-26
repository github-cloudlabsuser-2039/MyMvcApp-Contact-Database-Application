using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

        // GET: User/SearchByName
        public ActionResult SearchByName(string search)
        {
            // Check if the search term is null or empty
            if (string.IsNullOrEmpty(search))
            {
                // Return the full user list if no search term is provided
                return View("Index", userlist);
            }

            // Filter the userlist by name (case-insensitive)
            var filteredUsers = userlist.Where(u => u.Name.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            // Return the filtered list to the Index view
            return View("Index", filteredUsers);
        }

        // GET: User
        public ActionResult Index()
        {
            // Implement the Index method here
            return View(userlist);
        }
        

        /// GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Find the user in the userlist by ID
        var user = userlist.FirstOrDefault(u => u.Id == id);

        // If the user is not found, return a NotFound result
        if (user == null)
        {
            return NotFound();
        }

        // Pass the user to the Details view
        return View(user);
    }

        // GET: User/Create
        public ActionResult Create()
        {
            // Return the Create view to allow the user to input details for a new user
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                // Add the new user to the userlist
                userlist.Add(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            // Retrieve the user from the userlist based on the provided ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is not found, return a NotFound result
            if (user == null)
            {
                return NotFound();
            }

            // Pass the user to the Edit view
            return View(user);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            // Retrieve the user from the userlist based on the provided ID
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is not found, return a NotFound result
            if (existingUser == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Update the user's properties
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                

                // Redirect to the Index action to display the updated list of users
                return RedirectToAction("Index");
            }

            // If the model state is invalid, return the Edit view with the user data
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            // Retrieve the user from the userlist based on the provided ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is not found, return a NotFound result
            if (user == null)
            {
                return NotFound();
            }

            // Pass the user to the Delete view for confirmation
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            // Retrieve the user from the userlist based on the provided ID
            var user = userlist.FirstOrDefault(u => u.Id == id);

            // If the user is not found, return a NotFound result
            if (user == null)
            {
                return NotFound();
            }

            // Remove the user from the userlist
            userlist.Remove(user);

            // Redirect to the Index action to display the updated list of users
            return RedirectToAction("Index");
        }
}
