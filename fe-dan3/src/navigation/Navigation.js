import React from 'react';
import { NavLink as RRNavLink } from 'react-router-dom';
import { Collapse, Nav, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';


class Navigation extends React.Component{
  constructor(props){
    super(props);
    this.state = {isOpen: false}
  }

  toggle = () => this.setState({isOpen: !this.state.isOpen});

  render(){
    return (<div>
      <Navbar color="dark" dark expand="md">
        <div className="container"><NavbarBrand href="/">Library</NavbarBrand>
        <NavbarToggler onClick={this.toggle} />
        <Collapse isOpen={this.state.isOpen} navbar>
          <Nav className="ms-auto" navbar>
            <NavItem>
              <NavLink tag={RRNavLink} to="/authors" activeClassName="active">Authors</NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={RRNavLink} to="/books" activeClassName="active">Books</NavLink>
            </NavItem>
          </Nav>
        </Collapse>
      </div></Navbar>
        
    </div>);
  }
  
}

export default Navigation;