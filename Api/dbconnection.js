const DATABASE_NAME = 'NYlCaaoKUe';
const DATABASE_HOST = 'remotemysql.com';
const DATABASE_PORT = 3306;
const DATABASE_USER = 'NYlCaaoKUe';
const DATABASE_PASSWORD = 'y0qRXKlvFu';


function getAllUsers(){
    var result = [];
    var mysql      = require('mysql');
    var connection = mysql.createConnection({
    host     : DATABASE_HOST,
    port     : DATABASE_PORT,
    user     : DATABASE_USER,
    password : DATABASE_PASSWORD
    });
    connection.connect();

    connection.query(`USE ${DATABASE_NAME}`);
    connection.query('SELECT * FROM `User`', function(err, rows, fields) {
      if (err) throw err;
      for(var i = 0; i < rows.length; i++){
          result.push({
              username: rows[i].Username,
              password: rows[i].Password,
              type: rows[i].Type
          });
      }
    });
    
    connection.end();
    return result;
} 
console.log(getAllUsers());