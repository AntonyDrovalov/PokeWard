module.exports = app => {
    const users = require("../controllers/user.controller.js");
  
    // Create a new User
    app.post("/users", users.create);
  
    // Retrieve all Users
    app.get("/users", users.findAll);
  
    // Retrieve a single User with Username
    app.get("/users/:username", users.findOne);

    app.get("/userandtype/:username", users.findUserWithType);
  
    // Update a User with Username
    app.put("/users/:username", users.update);
  
    // Delete a User with Username
    app.delete("/users/:username", users.delete);
  
    // Create all Users
    app.delete("/users", users.deleteAll);
  };