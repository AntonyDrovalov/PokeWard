const express = require('express');
const fs = require('fs');
const path = require('path');

const router = express.Router();

const getUser = async (req, res, next) => {
  try {
    const data = fs.readFileSync(path.join(__dirname, './users.json'));
    const users = JSON.parse(data);
    const userFound = users.find(user => user.username === String(req.params.username));
    if (!userFound) {
      const err = new Error('User not found');
      err.status = 404;
      throw err;
    }
    res.json(userFound);
  } catch (e) {
    next(e);
  }
};

const getAllUsers = async (req, res, next) => {
  try {
    const data = fs.readFileSync(path.join(__dirname, './users.json'));
    const users = JSON.parse(data);
    res.json(users);
  } catch (e) {
    next(e);
  }
};

const createUser = async (req, res, next) => {
    try {
      const data = fs.readFileSync(path.join(__dirname, './users.json'));
      const user = JSON.parse(data);
      const newUser = {
        username: req.body.username,
        password: req.body.password,
      };
      user.push(newUser);
      fs.writeFileSync(path.join(__dirname, './users.json'), JSON.stringify(user));
      res.status(201).json(newUser);
    } catch (e) {
      next(e);
    }
  };

const updateUsers = async (req, res, next) => {
    try {
      const data = fs.readFileSync(path.join(__dirname, './users.json'));
      const users = JSON.parse(data);
      const userFound = stats.find(user => user.username === String(req.params.username));
      if (!userFound) {
        const err = new Error('User not found');
        err.status = 404;
        throw err;
      }
      const newUserData = {
        username: req.body.username,
        password: req.body.password,
      };
      const newUser = users.map(user => {
        if (user.username === String(req.params.username)) {
          return newUserData;
        } else {
          return user;
        }
      });
      fs.writeFileSync(path.join(__dirname, './users.json'), JSON.stringify(newUser));
      res.status(200).json(newUserData);
    } catch (e) {
      next(e);
    }
  };

const deleteUser = async (req, res, next) => {
try {
        const data = fs.readFileSync(path.join(__dirname, './users.json'));
        const users = JSON.parse(data);
        const userFound = stats.find(user => user.username === String(req.params.username));
        if (!userFound) {
            const err = new Error('Users not found');
            err.status = 404;
            throw err;
        }
        const newUser = stats.map(user => {
            if (user.username === String(req.params.username)) {
                return null;
            } else {
                return user;
            }
        })
        .filter(user => user !== null);
        fs.writeFileSync(path.join(__dirname, './users.json'), JSON.stringify(newUser));
        res.status(200).end();
    } catch (e) {
        next(e);
    }
};


router
  .route('/api/v1/users/:username')
  .get(getUser)
  .put(updateUsers)
  .delete(deleteUser);
  
router
.route('/api/v1/users')
.post(createUser)
.get(getAllUsers);

module.exports = router;
