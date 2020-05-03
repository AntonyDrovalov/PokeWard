const mysql = require("mysql");
const dbConfig = require("../config/db.config.js");

// Create a connection to the database
var connection;
connectDB();

// open the MySQL connection
connection.connect(error => {
  if (error) throw connectDB();
  console.log("Successfully connected to the database.");
});

module.exports = connection;

function connectDB(){
  connection = mysql.createConnection({
    host: dbConfig.HOST,
    user: dbConfig.USER,
    port: dbConfig.PORT,
    password: dbConfig.PASSWORD,
    database: dbConfig.DB
  });
}