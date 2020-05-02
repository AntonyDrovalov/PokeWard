module.exports = app => {
    const users = require("../controllers/user.controller.js");
  
    // Create a new Customer
    app.post("/users", users.create);
  
    // Retrieve all Customers
    app.get("/users", users.findAll);
  
    // Retrieve a single Customer with Username
    app.get("/users/:Username", users.findOne);
  
    // Update a Customer with Username
    app.put("/users/:Username", users.update);
  
    // Delete a Customer with Username
    app.delete("/users/:Username", users.delete);
  
    // Create a new Customer
    app.delete("/users", users.deleteAll);
  };