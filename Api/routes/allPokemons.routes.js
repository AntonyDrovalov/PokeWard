module.exports = app => {
    const allPokemons = require("../controllers/allPokemons.controller.js");
  
    app.post("/all-pokemons", allPokemons.create);
  
    app.get("/all-pokemons", allPokemons.findAll);
  
    app.get("/all-pokemons/:model", allPokemons.findOne);

    app.put("/all-pokemons/:model", allPokemons.update);
  };