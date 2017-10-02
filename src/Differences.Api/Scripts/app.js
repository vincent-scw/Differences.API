import React from 'react';
import ReactDOM from 'react-dom';
import GraphiQL from 'graphiql';
import fetch from 'isomorphic-fetch';
import 'graphiql/graphiql.css';
import './app.css';

function graphQLFetcher(graphQLParams) {
    if (graphQLParams.variables === undefined || graphQLParams.variables === "" || graphQLParams.variables === "null")
        graphQLParams.variables = {};
    else
        graphQLParams.variables = JSON.parse(graphQLParams.variables);
    return fetch(window.location.origin + '/api/graphql', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(graphQLParams)
    }).then(response => response.json());
}

ReactDOM.render(<GraphiQL fetcher={graphQLFetcher} />, document.getElementById('app'));