import React from "react";
import "./AddTodo.css";

class AddTodo extends React.Component {
  constructor(props) {
    super(props);
    this.state = { description: "" };
  }
  handleDescriptionChange = (event) => {
    this.setState({ description: event.target.value });
  };

  render() {
    return (
      <div>
        <input type="text" value={this.state.description} onChange={this.handleDescriptionChange} />
        <button
          className="addButton"
          onClick={() => this.props.onAdd({ description: this.state.description, status: "open" })}
        >
          Add
        </button>
      </div>
    );
  }
}

export default AddTodo;
