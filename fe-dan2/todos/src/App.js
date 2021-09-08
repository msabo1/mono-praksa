import React from "react";
import AddTodo from "./AddTodo";
import "./App.css";
import Header from "./Header";
import Todo from "./Todo";

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = { todos: [] };
  }

  id = 1;

  addTodo = (todo) => {
    todo.id = this.id++;
    this.setState({ todos: [...this.state.todos, todo] });
  };

  updateTodo = (todo) => {
    this.setState({ todos: this.state.todos.map((t) => (t.id === todo.id ? todo : t)) });
  };

  deleteTodo = (todo) => {
    this.setState({ todos: this.state.todos.filter((t) => t.id !== todo.id) });
  };

  render() {
    return (
      <div className="App">
        <Header title="Todos" />
        <div className="container">
          <div className="content">
            <div>
              <AddTodo onAdd={this.addTodo} />
            </div>
            {this.state.todos.map((todo) => (
              <Todo todo={todo} onUpdate={this.updateTodo} onDelete={this.deleteTodo} />
            ))}
          </div>
        </div>
      </div>
    );
  }
}

export default App;
