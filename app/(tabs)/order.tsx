import React, { useState } from 'react';
import { ScrollView, StyleSheet, View, Text, TouchableOpacity, Modal, TextInput } from 'react-native';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';
import { Picker } from '@react-native-picker/picker';
import { useFirebaseDatabase } from '../hooks/useDatabase';

const ordersData2 = [
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
  const [activeTab, setActiveTab] = useState('unsolved'); // Tab: 'pending' or 'history'
  const [description, setDescription] = useState('');
  const [requestType, setRequestType] = useState('public'); // Default to 'public'
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [sortOption, setSortOption] = useState('date-asc'); // Sorting by date


  const {data: firebaseData, loading} = useFirebaseDatabase('Orders');


  const ordersData = firebaseData
    ? Object.entries(firebaseData).map(([key, value]) => ({
        id: key,
        date: value.create_at, // Adjusted for Firebase key
        description: value.ord_description,
        status: value.ord_status,
        type: value.type,
      }))
    : [];

  // Filter orders based on tab
  const filteredOrders = ordersData.filter(
    (order) => (activeTab === 'unsolved' && order.status === 'Unsolved') 
    || (activeTab === 'history' && order.status !== 'Unsolved')
  );

  // Sort orders based on selected option
  const sortedOrders = filteredOrders.sort((a, b) => {
    switch (sortOption) {
      case 'date-asc':
        return new Date(a.date).getTime() - new Date(b.date).getTime();
      case 'date-desc':
        return new Date(b.date).getTime() - new Date(a.date).getTime();
      default:
        return 0;
    }
  });

  const handleSendRequest = () => {
    // Logic to send request (e.g., API call)
    console.log('Request sent:', { description, requestType });
    setIsModalVisible(false); // Close modal after sending
    setDescription(''); // Clear input fields
    setRequestType('public'); // Reset combo box
  };

  return (
    <ThemedView style={styles.container}>
      <View style={styles.tabContainer}>
        <TouchableOpacity
          style={[styles.tab, activeTab === 'unsolved' && styles.activeTab]}
          onPress={() => setActiveTab('unsolved')}
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

      {/* Sort options */}
      <View style={styles.sortContainer}>
        <TouchableOpacity onPress={() =>
         setSortOption(sortOption === 'date-asc' ? 'date-desc' : 'date-asc')} 
        style={styles.sortButton}>
          <ThemedText style={styles.sortText}>
            Date {sortOption === 'date-asc' ? '↓' : '↑'}</ThemedText>
        </TouchableOpacity>
      </View>




 {/* Loading Indicator */}


 {loading ? (
       <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
       <ThemedText>Loading orders...</ThemedText>
     </View>
   ) : (



    <ScrollView style={styles.ordersContainer}>
    {sortedOrders.map((order) => (
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
)}
      {/* Floating Action Button */}
      <TouchableOpacity
        style={styles.fab}
        onPress={() => setIsModalVisible(true)}
      >
        <ThemedText style={styles.fabText}>+</ThemedText>
      </TouchableOpacity>

      {/* Modal for sending request */}
      <Modal
        visible={isModalVisible}
        animationType="slide"
        transparent={true}
        onRequestClose={() => setIsModalVisible(false)}
      >
        <View style={styles.modalOverlay}>
          <View style={styles.modalContainer}>
            <Text style={styles.modalTitle}>Send Order</Text>

            {/* Description Input */}
            <TextInput
              style={styles.input}
              placeholder="Order Description"
              value={description}
              onChangeText={setDescription}
              multiline={true}
            />

            {/* Request Type Picker */}
            <Text style={styles.label}>Order Type</Text>
            <Picker
              selectedValue={requestType}
              style={styles.picker}
              onValueChange={(itemValue) => setRequestType(itemValue)}
            >
              <Picker.Item label="Public" value="public" />
              <Picker.Item label="Residence" value="residence" />
            </Picker>

            {/* Send Button */}
            <TouchableOpacity style={styles.sendButton} onPress={handleSendRequest}>
              <ThemedText style={styles.sendButtonText}>Send</ThemedText>
            </TouchableOpacity>

            {/* Close Button */}
            <TouchableOpacity
              style={styles.closeButton}
              onPress={() => setIsModalVisible(false)}
            >
              <ThemedText style={styles.closeButtonText}>Close</ThemedText>
            </TouchableOpacity>
          </View>
        </View>
      </Modal>
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
  sortContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 20,
  },
  sortButton: {
    backgroundColor: '#391f6c',
    paddingVertical: 8,
    paddingHorizontal: 15,
    borderRadius: 15,
  },
  sortText: {
    fontSize: 16,
    color: 'white',
    fontWeight: 'bold',
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
  fab: {
    position: 'absolute',
    bottom: 20,
    right: 20,
    backgroundColor: '#7369ff',
    width: 60,
    height: 60,
    borderRadius: 30,
    justifyContent: 'center',
    alignItems: 'center',
    elevation: 6,
  },
  fabText: {
    fontSize: 30,
    color: 'white',
    fontWeight: 'bold',
  },
  modalOverlay: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: 'rgba(0, 0, 0, 0.5)',
  },
  modalContainer: {
    backgroundColor: 'white',
    borderRadius: 10,
    padding: 20,
    width: '80%',
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 5,
  },
  modalTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 15,
    textAlign: 'center',
  },
  input: {
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
    padding: 10,
    marginBottom: 15,
    fontSize: 16,
    textAlignVertical: 'top',
  },
  label: {
    fontSize: 16,
    fontWeight: '500',
    color: '#555',
    marginBottom: 10,
  },
  picker: {
    marginBottom: 20,
    height: 50,
    borderColor: '#ccc',
    borderWidth: 1,
    borderRadius: 5,
  },
  sendButton: {
    backgroundColor: '#7369ff',
    paddingVertical: 12,
    paddingHorizontal: 20,
    borderRadius: 30,
    alignItems: 'center',
  },
  sendButtonText: {
    color: 'white',
    fontSize: 16,
    fontWeight: '600',
  },
  closeButton: {
    marginTop: 15,
    alignItems: 'center',
  },
  closeButtonText: {
    color: '#7369ff',
    fontSize: 16,
    fontWeight: '600',
  },
});

function getStatusStyle(status: String) {
  switch (status) {
    case 'Unsolved':
      return { color: 'orange' };
    case 'Solved':
      return { color: 'green' };
    case 'In Progress':
      return { color: 'blue' };
    default:
      return { color: 'gray' };
  }
}
