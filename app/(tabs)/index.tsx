import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { NavigationContainer } from '@react-navigation/native';
import { Link } from 'expo-router';
import Order from './order';
import EmployeeInfo from './information';
import React from 'react';
import { Text, StyleSheet } from 'react-native';
import { Provider } from 'react-redux';
import store from '../store';

const Tab = createBottomTabNavigator();

const TabNavigation = () => {
  return (
    <Provider store={store}>  

    <Tab.Navigator
      initialRouteName="Order"
      screenOptions={{
        tabBarStyle: { backgroundColor: '#1e2e44' },  // Đổi màu nền tab bar
        tabBarActiveTintColor: '#44FEA1',  // Màu active của tab
        tabBarInactiveTintColor: '#FFF',  // Màu không active của tab
      }}
    >
      <Tab.Screen
        name="Order"
        component={Order}
        
        options={{
          headerTitle: 'Order',
          tabBarLabel: 'Order',
          headerStyle: {
            backgroundColor: '#1e2e44', // Màu nền header cho tab Order
          },
          headerTintColor: '#fff',
          headerPressColor:'#1e2e44',         
        }}
        
      />
      <Tab.Screen
        name="EmployeeInfo"
        component={EmployeeInfo}
        options={{
          headerTitle: 'Thông tin nhân viên',
          tabBarLabel: 'Thông tin',
          headerStyle: {
            backgroundColor: '#1e2e44', // Màu nền header cho tab Order
          },
          headerTintColor: '#fff',
        }}
      />
    </Tab.Navigator>
    </Provider>
  );
};

export default TabNavigation;
