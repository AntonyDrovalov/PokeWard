const Usertype = require("../models/usertype.model.js");

// Create and Save a new Type
exports.create = (req, res) => {
    // Validate request
    if (!req.body) {
        res.status(400).send({
          message: "Content can not be empty!"
        });
      }
    
      const usertype = new Usertype({
        username: req.body.username,
        typeId: req.body.typeId
      });
    
      // Save Type in the database
      Usertype.create(usertype, (err, data) => {
        if (err)
          res.status(500).send({
            message:
              err.message || "Some error occurred while creating the Type."
          });
        else res.send(data);
      });
};

// Retrieve all Types from the database.
exports.findAll = (req, res) => {
    Usertype.getAll((err, data) => {
        if (err)
          res.status(500).send({
            message:
              err.message || "Some error occurred while retrieving types."
          });
        else res.send(data);
      });
};

// Find a single Typa with a id
exports.findOne = (req, res) => {
    Usertype.findById(req.params.username, (err, data) => {
        if (err) {
          if (err.kind === "not_found") {
            res.status(404).send({
              message: `Not found type with username ${req.params.username}.`
            });
          } else {
            res.status(500).send({
              message: "Error retrieving type with username" + req.params.username
            });
          }
        } else res.send(data);
      });
};
