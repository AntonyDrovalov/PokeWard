module.exports = app => {
    const usertype = require("../controllers/usertype.controller.js");
  
    app.post("/usertype", usertype.create);
  
    app.get("/usertype", usertype.findAll);
  
    app.get("/usertype/:username", usertype.findOne);
  };