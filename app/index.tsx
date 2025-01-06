import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import LoginScreen from './login';
import { createStaticNavigation, NavigationContainer } from '@react-navigation/native';
import Login from './login';
import EmployeeInfo from './(tabs)/information';
import { Provider } from 'react-redux';
import store from './store';


export default function App() {
  return (
    <Provider store={store}>  
      <Login  />
    </Provider>
  );
}
