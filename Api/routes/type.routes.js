module.exports = app => {
    const types = require("../controllers/type.controller.js");
  
    app.post("/types", types.create);
  
    app.get("/types", types.findAll);
  
    app.get("/types/:id", types.findOne);
  };