﻿import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

interface Contact {
    id: number;
    name: string;
    address: string;
    city: string;
    state: string;
    postalCode: string;
    phone: string;
    email: string;
}

interface ContactListState {
    contacts: Contact[];
    loading: boolean;
}

export class ContactList extends React.Component<RouteComponentProps<{}>, ContactListState>{
    constructor() {
        super();
        this.state = { contacts: [], loading: true }

        fetch('http://localhost:50191/api/contractApi')
            .then(response => response.json() as Promise<Contact[]>)
            .then(data => {
                this.setState({ contacts: data, loading: false });
        })
    }

    public render() {
        const hasContact = this.state.contacts.length > 0;

        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : hasContact ? ContactList.renderContactsTable(this.state.contacts)
                : <p>No contact</p>;

        return <div>
            <h1>Contact List</h1>
            {contents}
        </div>
    }

    private static renderContactsTable(contacts: Contact[]) {
        return <table className='table'>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                </tr>
            </thead>
            <tbody>
                {contacts.map(contact =>
                    <tr key={contact.id}>
                        <td>{contact.id}</td>
                        <td>{contact.name}</td>
                    </tr>
                )}
            </tbody>
        </table>
    }
}