import React, { useState } from 'react';
import { ScrollView, StyleSheet, View, Text, TouchableOpacity } from 'react-native';
import { ThemedText } from '@/components/ThemedText';
import { ThemedView } from '@/components/ThemedView';
import { useFirebaseDatabase } from '../hooks/useDatabase';

// const receiptsData = [
//   {
//     id: 1,
//     date: '2025-01-01',
//     description: 'Receipt for purchase of products',
//     amount: 150,
//     status: 'Unpaid',
//     type: 'Purchase',
//   },
//   {
//     id: 2,
//     date: '2025-01-02',
//     description: 'Receipt for subscription renewal',
//     amount: 50,
//     status: 'Paid',
//     type: 'Subscription',
//   },
//   {
//     id: 3,
//     date: '2025-01-03',
//     description: 'Receipt for service fee',
//     amount: 200,
//     status: 'Unpaid',
//     type: 'Service',
//   },
// ];



export default function ReceiptScreen() {
  const [activeTab, setActiveTab] = useState('unpaid');
  const [sortOption, setSortOption] = useState('date-asc'); 

  const {data: firebaseData, loading} = useFirebaseDatabase('Bills');

  const receiptsData = firebaseData
    ? Object.entries(firebaseData).map(([key, value]) => ({
        id: key,
        date: value.create_at, 
        description: value.bill_description,
        amount: value.amount,
        status: value.bill_status,
        type: value.bill_type,

    })) 
    : [];

  const filteredReceipts = receiptsData.filter(
    (receipt) => (activeTab === 'unpaid' && receipt.status === 'Unpaid') || (activeTab === 'history' && receipt.status !== 'Unpaid')
  );

  const sortedReceipts = filteredReceipts.sort((a, b) => {
    switch (sortOption) {
      case 'date-asc':
        return new Date(a.date).getTime() - new Date(b.date).getTime();
      case 'date-desc':
        return new Date(b.date).getTime() - new Date(a.date).getTime();
      case 'amount-asc':
        return a.amount - b.amount;
      case 'amount-desc':
        return b.amount - a.amount;
      default:
        return 0;
    }
  });

  return (
    <ThemedView style={styles.container}>
      <View style={styles.tabContainer}>
        <TouchableOpacity
          style={[styles.tab, activeTab === 'unpaid' && styles.activeTab]}
          onPress={() => setActiveTab('unpaid')}
        >
          <ThemedText style={styles.tabText}>Receipts Unpaid</ThemedText>
        </TouchableOpacity>
        <TouchableOpacity
          style={[styles.tab, activeTab === 'history' && styles.activeTab]}
          onPress={() => setActiveTab('history')}
        >
          <ThemedText style={styles.tabText}>Receipt History</ThemedText>
        </TouchableOpacity>
      </View>


     

      {/* Sort options */}
      <View style={styles.sortContainer}>
        <TouchableOpacity onPress={() => setSortOption(sortOption === 'date-asc' ? 'date-desc' : 'date-asc')} style={styles.sortButton}>
          <ThemedText style={styles.sortText}>Date {sortOption === 'date-asc' ? '↓' : '↑'}</ThemedText>
        </TouchableOpacity>
        <TouchableOpacity onPress={() => setSortOption(sortOption === 'amount-asc' ? 'amount-desc' : 'amount-asc')} style={styles.sortButton}>
          <ThemedText style={styles.sortText}>Amount {sortOption === 'amount-asc' ? '↓' : '↑'}</ThemedText>
        </TouchableOpacity>
      </View>

      {/* Display sorted receipts */}
      {/* <ScrollView style={styles.receiptsContainer}>
        {sortedReceipts.map((receipt) => (
          <View key={receipt.id} style={styles.receiptBox}>
            <Text style={styles.receiptDate}>{receipt.date}</Text>
            <Text style={styles.receiptType}>{receipt.type}</Text>
            <Text style={styles.receiptDescription}>{receipt.description}</Text>
        
            <Text style={[styles.receiptStatus, getStatusStyle(receipt.status)]}>{receipt.status}</Text>
          </View>
        ))}
      </ScrollView> */}

      {loading ? (
             <View style={{ flex: 1, justifyContent: 'center', alignItems: 'center' }}>
             <ThemedText>Loading receipts...</ThemedText>
           </View>
         ) : (
      
      
      
          <ScrollView style={styles.receiptsContainer}>
          {sortedReceipts.map((receipt) => (
            <View key={receipt.id} style={styles.receiptBox}>
              <Text style={styles.receiptDate}>{receipt.date}</Text>
              <Text style={styles.receiptDescription}>{receipt.description}</Text>
              <Text style={styles.receiptAmount}>{receipt.amount} VND</Text>
              <Text style={[styles.receiptStatus, getStatusStyle(receipt.status)]}>
                {receipt.status}
              </Text>
              <Text style={styles.receiptType}>{receipt.type}</Text>
            </View>
          ))}
        </ScrollView>
      )}
    </ThemedView>
  );
}

function getStatusStyle(status: string) {
  switch (status) {
    case 'Unpaid':
      return { color: 'red' };
    case 'Paid':
      return { color: 'green' };
    default:
      return { color: 'gray' };
  }
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
  receiptsContainer: {
    flex: 1,
  },
  receiptBox: {
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
  receiptDate: {
    fontSize: 16,
    fontWeight: '500',
    color: '#555',
  },
  receiptType: {
    fontSize: 14,
    color: '#7369ff',
    marginVertical: 5,
  },
  receiptDescription: {
    fontSize: 14,
    color: '#333',
    marginVertical: 5,
  },
  receiptAmount: {
    fontSize: 16,
    fontWeight: '600',
    color: '#333',
    marginVertical: 5,
  },
  receiptStatus: {
    fontSize: 14,
    fontWeight: '600',
    marginVertical: 5,
  },
});
