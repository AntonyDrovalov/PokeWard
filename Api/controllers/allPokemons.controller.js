const AllPokemons = require("../models/allPokemons.model.js");

// Create and Save a new AllPokemon
exports.create = (req, res) => {
    // Validate request
    if (!req.body) {
        res.status(400).send({
          message: "Content can not be empty!"
        });
      }
    
      const allPokemon = new AllPokemons({
        model: req.body.model,
        name: req.body.name
      });
    
      AllPokemons.create(allPokemon, (err, data) => {
        if (err)
          res.status(500).send({
            message:
              err.message || "Some error occurred while creating the pokemon model."
          });
        else res.send(data);
      });
};

exports.findAll = (req, res) => {
    AllPokemons.getAll((err, data) => {
        if (err)
          res.status(500).send({
            message:
              err.message || "Some error occurred while retrieving pokemon models."
          });
        else res.send(data);
      });
};

exports.findOne = (req, res) => {
    AllPokemons.findByModel(req.params.model, (err, data) => {
        if (err) {
          if (err.kind === "not_found") {
            res.status(404).send({
              message: `Not found pokemon with model ${req.params.model}.`
            });
          } else {
            res.status(500).send({
              message: "Error retrieving pokemon with model " + req.params.model
            });
          }
        } else res.send(data);
      });
};

exports.update = (req, res) => {
  // Validate Request
  if (!req.body) {
      res.status(400).send({
        message: "Content can not be empty!"
      });
    }
    console.log(req.body);
    AllPokemons.updateByModel(
      req.params.model,
      new AllPokemons(req.body),
      (err, data) => {
        if (err) {
          if (err.kind === "not_found") {
            res.status(404).send({
              message: `Not found Pokemon with Model ${req.params.model}.`
            });
          } else {
            res.status(500).send({
              message: "Error updating Pokemon with Model" + req.params.model
            });
          }
        } else res.send(data);
      }
    );
};