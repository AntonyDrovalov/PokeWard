module.exports = app => {
    const users = require("../controllers/user.controller.js");
  
    // Create a new User
    app.post("/users", users.create);
  
    // Retrieve all Users
    app.get("/users", users.findAll);
  
    // Retrieve a single User with Username
    app.get("/users/:Username", users.findOne);
  
    // Update a User with Username
    app.put("/users/:Username", users.update);
  
    // Delete a User with Username
    app.delete("/users/:Username", users.delete);
  
    // Create all Users
    app.delete("/users", users.deleteAll);
  };