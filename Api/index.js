const express = require("express");
const bodyParser = require("body-parser");
const PORT = process.env.PORT || 3000;

const app = express();

// parse requests of content-type: application/json
app.use(bodyParser.json());

// parse requests of content-type: application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: true }));

// simple route
app.get("/", (req, res) => {
  res.json({ message: "Welcome to PokeWard application." });
});

require("./routes/user.routes.js")(app);
require("./routes/type.routes.js")(app);
require("./routes/allPokemons.routes.js")(app);

// set port, listen for requests
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}.`);
});
