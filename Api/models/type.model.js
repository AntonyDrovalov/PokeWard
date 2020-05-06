const sql = require("./db.js");

// constructor
const Type = function(type) {
  this.id = type.id;
  this.type = type.type;
};

Type.create = (newType, result) => {
  sql.query("INSERT INTO Type SET ?", newType, (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(err, null);
      return;
    }

    console.log("created type: ", { id: res.insertId, ...newUser });
    result(null, { id: res.insertId, ...newUser });
  });
};

Type.findById = (typeId, result) => {
  sql.query(`SELECT * FROM Type WHERE id = "${typeId}"`, (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(err, null);
      return;
    }

    if (res.length) {
      console.log("found type: ", res[0]);
      result(null, res[0]);
      return;
    }

    result({ kind: "not_found" }, null);
  });
};

Type.getAll = result => {
  sql.query("SELECT * FROM Type", (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(null, err);
      return;
    }

    console.log("Type: ", res);
    result(null, res);
  });
};

module.exports = Type;