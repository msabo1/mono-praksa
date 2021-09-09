import Authors from 'authors/Authors';
import React from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import './App.css';
import Navigation from './navigation/Navigation';

function App() {
  return (
      <Router>
        <Navigation />
        <Switch>
          <Route exact path="/" component={Authors} />
          <Route exact path="/authors" component={Authors} />
        </Switch>
      </Router>
  );
}

export default App;
