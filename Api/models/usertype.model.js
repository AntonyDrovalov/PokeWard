const sql = require("./db.js");

// constructor
const Usertype = function(usertype) {
  this.username = usertype.username;
  this.typeId = usertype.typeId;
};

Usertype.create = (usertype, result) => {
  sql.query("INSERT INTO UserType SET ?", usertype, (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(err, null);
      return;
    }

    console.log("created type: ", { id: res.insertId, ...newUser });
    result(null, { id: res.insertId, ...newUser });
  });
};

Usertype.findById = (username, result) => {
  sql.query(`SELECT * FROM UserType WHERE username = "${username}"`, (err, res) => {
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

Usertype.getAll = result => {
  sql.query("SELECT * FROM UserType", (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(null, err);
      return;
    }

    console.log("Type: ", res);
    result(null, res);
  });
};

module.exports = Usertype;