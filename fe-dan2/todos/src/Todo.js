import React from "react";
import "./Todo.css";

class Todo extends React.Component {
  constructor(props) {
    super(props);
    this.state = { enableEdit: false, description: this.props.todo.description };
  }
  render() {
    return (
      <div className="todo">
        <div className="todo-description">
          <span
            style={{
              textDecoration: this.props.todo.status === "done" ? "line-through" : "none",
              display: !this.state.enableEdit ? "initial" : "none",
            }}
          >
            {this.props.todo.description}
          </span>
          <input
            type="text"
            value={this.state.description}
            onChange={(e) => this.setState({ description: e.target.value })}
            style={{ display: this.state.enableEdit ? "initial" : "none" }}
          />
          <span
            className="material-icons edit"
            style={{ display: this.state.enableEdit ? "initial" : "none" }}
            onClick={() => {
              this.setState({ enableEdit: false });
              this.props.todo.description = this.state.description;
              this.props.onUpdate(this.props.todo);
            }}
          >
            save
          </span>
        </div>
        <div>
          <span className="material-icons edit" onClick={() => this.setState({ enableEdit: true })}>
            edit
          </span>
          <span
            className="material-icons done"
            style={{ display: this.props.todo.status === "open" ? "initial" : "none" }}
            onClick={() => {
              this.props.todo.status = "done";
              this.props.onUpdate(this.props.todo);
            }}
          >
            done
          </span>
          <span
            className="material-icons done"
            style={{ display: this.props.todo.status === "done" ? "initial" : "none" }}
            onClick={() => {
              this.props.todo.status = "open";
              this.props.onUpdate(this.props.todo);
            }}
          >
            restore
          </span>
          <span
            className="material-icons delete"
            onClick={() => {
              this.props.onDelete(this.props.todo);
            }}
          >
            delete
          </span>
        </div>
      </div>
    );
  }
}

export default Todo;
