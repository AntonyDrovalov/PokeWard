const Type = require("../models/type.model.js");

// Create and Save a new Type
exports.create = (req, res) => {
    // Validate request
    if (!req.body) {
        res.status(400).send({
          message: "Content can not be empty!"
        });
      }
    
      // Create a User
      const type = new Type({
        id: req.body.id,
        type: req.body.type
      });
    
      // Save Type in the database
      Type.create(type, (err, data) => {
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
    Type.getAll((err, data) => {
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
    Type.findById(req.params.id, (err, data) => {
        if (err) {
          if (err.kind === "not_found") {
            res.status(404).send({
              message: `Not found type with id ${req.params.id}.`
            });
          } else {
            res.status(500).send({
              message: "Error retrieving type with id" + req.params.id
            });
          }
        } else res.send(data);
      });
};
