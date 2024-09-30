import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { queueGroups: [], loading: true, isLoggedIn: !!localStorage.getItem('token') };
    }

    componentDidMount() {
        if (this.state.isLoggedIn) {
            this.populateQueueGroupData();
            this.interval = setInterval(() => this.populateQueueGroupData(), 10000); // Refresh every 10 seconds
        }
    }

    componentWillUnmount() {
        clearInterval(this.interval);
    }

    static renderQueueGroupsTable(queueGroups) {
        console.log("data", queueGroups);
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Queue Group Name</th>
                        <th>Offered</th>
                        <th>Handled</th>
                        <th>Average Talk Time</th>
                        <th>Average Handling Time</th>
                        <th>Service Level</th>
                    </tr>
                </thead>
                <tbody>
                    {queueGroups.map(group =>
                        <tr key={group.queueGroupName}>
                            <td>{group.queueGroupName}</td>
                            <td>{group.offered}</td>
                            <td>{group.handled}</td>
                            <td>{group.averageTalkTime}</td>
                            <td>{group.averageHandlingTime}</td>
                            <td style={{ color: group.serviceLevel < group.slA_Percent ? 'red' : 'green' }}>
                                {group.serviceLevel.toFixed(1)}%
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        if (!this.state.isLoggedIn) {
            return <Redirect to="/login" />;
        }

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderQueueGroupsTable(this.state.queueGroups);

        return (
            <div>
                <h1 id="tabelLabel">Queue Group Data</h1>
                <p>This component demonstrates fetching queue group data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateQueueGroupData() {
        const token = localStorage.getItem('token');
        const response = await fetch('/api/monitoring', {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
        const data = await response.json();
        this.setState({ queueGroups: data, loading: false });
    }
}
