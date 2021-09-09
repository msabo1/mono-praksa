import axios from 'axios';
import React from 'react';
import { Button, Navbar, Table } from 'reactstrap';
import EditAuthor from './EditAuthor';
import NewAuthor from './NewAuthor';

export default class Authors extends React.Component{
  constructor(props){
    super(props);
    this.state = {
      authors: [],
      createModalOpen: false,
      updateModalOpen: false,
      authorToUpdate: null,
    }
  }

  async componentDidMount(){
    await this.fetchAuthors();
  }

  async fetchAuthors(){
    this.setState({authors: (await axios.get('https://localhost:5001/api/authors')).data});
  }

  toggleCreateModal = () => {
    this.setState({createModalOpen: !this.state.createModalOpen});
  }
  toggleUpdateModal = () => {
    this.setState({updateModalOpen: !this.state.updateModalOpen});
  }

  onAuthorChange = () => {
    this.fetchAuthors();
  }

  onEditClick = (author) => {
    this.setState({updateModalOpen: !this.state.updateModalOpen, authorToUpdate: author});
  }

  onDeleteClick = async (author) => {
    await axios.delete(`https://localhost:5001/api/authors/${author.id}`);
    this.fetchAuthors();
  }

  render(){
    return <div>
      <Navbar color="secondary" secondary><h1 className='container text-light'>Authors</h1></Navbar>
      <div className="container mt-2">
        <Button color="success" onClick={this.toggleCreateModal}>Create new</Button>
        <Table hover>
          <thead>
            <tr>
              <th>Name</th>
              <th>Gender</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            {this.state.authors.map(author => (<tr key={author.id}>
              <td>{author.name}</td>
              <td>{author.gender}</td>
              <td>
                <Button color="primary" onClick={() => this.onEditClick(author)}>Edit</Button> 
                <Button color="danger" onClick={() => this.onDeleteClick(author)}>Delete</Button>
              </td>
              </tr>))}
          </tbody>
        </Table>
    </div>
    <NewAuthor open={this.state.createModalOpen} toggle={this.toggleCreateModal} onCreate={this.onAuthorChange} />
    {this.state.updateModalOpen ? <EditAuthor open={this.state.updateModalOpen} toggle={this.toggleUpdateModal} onCreate={this.onAuthorChange} author={this.state.authorToUpdate}/> : null}
    </div>
  }
}