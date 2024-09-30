import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link, Redirect } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.state = {
            collapsed: true,
            isAuthenticated: !!localStorage.getItem('token') // Initial authentication check
        };
    }

    toggleNavbar() {
        this.setState(prevState => ({
            collapsed: !prevState.collapsed
        }));
    }

    handleLogout = () => {
        // Clear local storage and update state
        localStorage.removeItem('token');
        this.setState({ isAuthenticated: false });
    };

    render() {
        const { isAuthenticated, collapsed } = this.state;

        // Redirect to login page if not authenticated
        if (!isAuthenticated) {
            return <Redirect to="/login" />;
        }

        return (
            <header>
                <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" light>
                    <Container>
                        <NavbarBrand tag={Link} to="/fetch-data">TestApplication</NavbarBrand>
                        <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
                        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
                            <ul className="navbar-nav flex-grow">
                                <NavItem>
                                    <NavLink tag={Link} className="text-dark" to="/fetch-data">Monitor QG</NavLink>
                                </NavItem>
                                <NavItem>
                                    <button className="btn btn-link text-dark" onClick={this.handleLogout}>Logout</button>
                                </NavItem>
                            </ul>
                        </Collapse>
                    </Container>
                </Navbar>
            </header>
        );
    }
}
