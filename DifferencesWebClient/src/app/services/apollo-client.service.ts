import { ApolloClient, createNetworkInterface } from 'apollo-client';
import { Config } from '../config';

const client = new ApolloClient({
  networkInterface: createNetworkInterface({
    uri: Config.GRAPHQL_API_ENDPOINT
  })
});

export function provideClient(): ApolloClient {
  return client;
}
