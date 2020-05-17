const sql = require("./db.js");

// constructor
const AllPokemons = function(allPokemon) {
  this.model = allPokemon.model;
  this.name = allPokemon.name;
};

AllPokemons.create = (newAllPokemon, result) => {
  sql.query("INSERT INTO AllPokemons SET ?", newAllPokemon, (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(err, null);
      return;
    }

    console.log("created pokemon model: ", { model: res.insertModel, ...newAllPokemon });
    result(null, { model: res.insertModel, ...newAllPokemon });
  });
};

AllPokemons.findByModel = (model, result) => {
  sql.query(`SELECT * FROM AllPokemons WHERE model = "${model}"`, (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(err, null);
      return;
    }

    if (res.length) {
      console.log("found pokemon model: ", res[0]);
      result(null, res[0]);
      return;
    }

    result({ kind: "not_found" }, null);
  });
};

AllPokemons.getAll = result => {
  sql.query("SELECT * FROM AllPokemons", (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(null, err);
      return;
    }

    console.log("All Pokemons: ", res);
    result(null, res);
  });
};

module.exports = AllPokemons;