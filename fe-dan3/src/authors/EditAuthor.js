import axios from 'axios';
import React from 'react';
import { Button, Form, FormGroup, Input, Label, Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';

export default class EditAuthor extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  componentDidMount(){
    this.setState({name: this.props.author?.name, gender: this.props.author.gender});
  }
  
  handleFormFieldChange = (event) => {
    const state = {};
    state[event.target.name] = event.target.value;
    this.setState(state);
  }

  updateAuthor = async () => {
    const author = (await axios.put(`https://localhost:5001/api/authors/${this.props.author.id}`, {
      name: this.state.name,
      gender: this.state.gender
    })).data;
    if(author.id){
      this.props.onCreate();
      this.props.toggle();
    }
  }

  isFormValid = () => {
    return (this.state.name != '') && (this.state.gender != '');
  }

  render() {
    return(
    <Modal isOpen={this.props.open} toggle={this.props.toggle} backdrop={true}>
      <ModalHeader toggle={this.props.toggle}>Edit author</ModalHeader>
      <ModalBody>
        <Form>
          <FormGroup>
            <div className="form-floating">
              <Input type="text" name="name" id="name" placeholder="Name"  value={this.state.name} onChange={this.handleFormFieldChange}/>
              <Label for="name">Name</Label>
            </div>
          </FormGroup>
          <FormGroup className="mt-2">
            <div className="form-floating">
              <Input type="text" name="gender" id="gender" placeholder="Gender" value={this.state.gender} onChange={this.handleFormFieldChange}/>
              <Label for="gender">Gender</Label>
            </div>
          </FormGroup>
        </Form>
      </ModalBody>
      <ModalFooter>
        <Button color="primary" onClick={this.updateAuthor} disabled={!this.isFormValid()}>Update</Button>{' '}
        <Button color="secondary" onClick={this.props.toggle}>Cancel</Button>
      </ModalFooter>
    </Modal>
    );
  }
}