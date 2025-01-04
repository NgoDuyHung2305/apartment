import React, { useState } from 'react';
import { ScrollView, StyleSheet, View, Text, TouchableOpacity } from 'react-native';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';

const ordersData = [
  {
    id: 1,
    date: '2025-01-01',
    description: 'Request to change delivery address',
    status: 'Pending',
    type: 'Delivery',
  },
  {
    id: 2,
    date: '2025-01-02',
    description: 'Request to cancel the order',
    status: 'Completed',
    type: 'Cancellation',
  },
  {
    id: 3,
    date: '2025-01-03',
    description: 'Request for a refund',
    status: 'In Progress',
    type: 'Refund',
  },
];

export default function OrderScreen() {
  const [activeTab, setActiveTab] = useState('pending'); // Tab: 'pending' or 'history'

  // Filter orders based on tab
  const filteredOrders = ordersData.filter(
    (order) => (activeTab === 'pending' && order.status === 'Pending') || (activeTab === 'history' && order.status !== 'Pending')
  );

  return (
    <ThemedView style={styles.container}>
      <View style={styles.tabContainer}>
        <TouchableOpacity
          style={[styles.tab, activeTab === 'pending' && styles.activeTab]}
          onPress={() => setActiveTab('pending')}
        >
          <ThemedText style={styles.tabText}>Unsolved Orders</ThemedText>
        </TouchableOpacity>
        <TouchableOpacity
          style={[styles.tab, activeTab === 'history' && styles.activeTab]}
          onPress={() => setActiveTab('history')}
        >
          <ThemedText style={styles.tabText}>Order History</ThemedText>
        </TouchableOpacity>
      </View>

      <ScrollView style={styles.ordersContainer}>
        {filteredOrders.map((order) => (
          <View key={order.id} style={styles.orderBox}>
            <Text style={styles.orderDate}>{order.date}</Text>
            <Text style={styles.orderDescription}>{order.description}</Text>
            <Text style={[styles.orderStatus, getStatusStyle(order.status)]}>
              {order.status}
            </Text>
            <Text style={styles.orderType}>{order.type}</Text>
          </View>
        ))}
      </ScrollView>
    </ThemedView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#1e2e44',
  },
  tabContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 20,
  },
  tab: {
    padding: 10,
    borderBottomWidth: 2,
    borderBottomColor: 'transparent',
  },
  activeTab: {
    borderBottomColor: 'white',
  },
  tabText: {
    fontSize: 18,
    fontWeight: 'bold',
    color: 'white',
  },
  ordersContainer: {
    flex: 1,
  },
  orderBox: {
    backgroundColor: '#fff',
    borderRadius: 10,
    padding: 15,
    marginBottom: 15,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  orderDate: {
    fontSize: 16,
    fontWeight: '500',
    color: '#555',
  },
  orderDescription: {
    fontSize: 14,
    color: '#333',
    marginVertical: 5,
  },
  orderStatus: {
    fontSize: 14,
    fontWeight: '600',
    marginVertical: 5,
  },
  orderType: {
    fontSize: 14,
    color: '#7369ff',
  },
});

function getStatusStyle(status: String) {
  switch (status) {
    case 'Pending':
      return { color: 'orange' };
    case 'Completed':
      return { color: 'green' };
    case 'In Progress':
      return { color: 'blue' };
    default:
      return { color: 'gray' };
  }
}
