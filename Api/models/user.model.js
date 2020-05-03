const sql = require("./db.js");

// constructor
const User = function(user) {
  this.Username = user.Username;
  this.Password = user.Password;
  this.Type = user.Type;
};

User.create = (newUser, result) => {
  sql.query("INSERT INTO User SET ?", newUser, (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(err, null);
      return;
    }

    console.log("created user: ", { Username: res.insertUsername, ...newUser });
    result(null, { Username: res.insertUsername, ...newUser });
  });
};

User.findByUsername = (userUsername, result) => {
  sql.query(`SELECT * FROM User WHERE Username = "${userUsername}"`, (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(err, null);
      return;
    }

    if (res.length) {
      console.log("found user: ", res[0]);
      result(null, res[0]);
      return;
    }

    // not found User with the Username
    result({ kind: "not_found" }, null);
  });
};

User.getAll = result => {
  sql.query("SELECT * FROM User", (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(null, err);
      return;
    }

    console.log("User: ", res);
    result(null, res);
  });
};

User.updateByUsername = (Username, user, result) => {
  sql.query(
    "UPDATE User SET Passwors = ?, Type = ? WHERE Username = ?",
    [user.Password, user.Type, Username],
    (err, res) => {
      if (err) {
        console.log("error: ", err);
        result(null, err);
        return;
      }

      if (res.affectedRows == 0) {
        // not found User with the Username
        result({ kind: "not_found" }, null);
        return;
      }

      console.log("updated user: ", { Username: Username, ...user });
      result(null, { Username: Username, ...user });
    }
  );
};

User.remove = (Username, result) => {
  sql.query("DELETE FROM User WHERE Username = ?", Username, (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(null, err);
      return;
    }

    if (res.affectedRows == 0) {
      // not found User with the Username
      result({ kind: "not_found" }, null);
      return;
    }

    console.log("deleted user with Username: ", Username);
    result(null, res);
  });
};

User.removeAll = result => {
  sql.query("DELETE FROM User", (err, res) => {
    if (err) {
      console.log("error: ", err);
      result(null, err);
      return;
    }

    console.log(`deleted ${res.affectedRows} User`);
    result(null, res);
  });
};

module.exports = User;