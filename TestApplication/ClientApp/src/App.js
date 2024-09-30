import React, { Component } from 'react';
import { Route, BrowserRouter as Router, Redirect, Switch } from 'react-router-dom';
import { Layout } from './components/Layout';
import { FetchData } from './components/FetchData';
import { Login } from './components/Login';
import { NavMenu } from './components/NavMenu';

import './custom.css';


export default class App extends Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
        this.state = {
            isLoggedIn: false,
        };
    }

    handleLogin = () => {
        this.setState({ isLoggedIn: true });
    };

    handleLogout = () => {
        this.setState({ isLoggedIn: false });
        localStorage.removeItem('token');
    };

    render() {
        const { isLoggedIn } = this.state;

        return (
            <Router>
                <Layout isLoggedIn={isLoggedIn} onLogout={this.handleLogout}>
                    <Switch>
                        {/* Redirect from root to /fetch-data */}
                        <Route exact path="/" render={() => <Redirect to="/fetch-data" />} />

                        <Route
                            path='/login'
                            render={(props) => (
                                isLoggedIn ? (
                                    <Redirect to='/fetch-data' />
                                ) : (
                                    <Login onLogin={this.handleLogin} {...props} />
                                )
                            )}
                        />
                        <Route path='/fetch-data' component={FetchData} />
                        {/* Optional: Add a catch-all route for unknown paths */}
                        <Route path="*" render={() => <Redirect to="/fetch-data" />} />
                    </Switch>
                </Layout>
            </Router>
        );
    }
}
