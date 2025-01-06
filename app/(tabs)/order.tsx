import React, { useState, useEffect } from "react";
import { View, Alert, Modal, ScrollView, Text, StyleSheet, ActivityIndicator } from "react-native";
import ModalCard from "./ordercard"; 
import ListCard from "./listcard";  
import { Provider, useDispatch, useSelector } from 'react-redux';  
import { useFirebaseDatabase } from "../hooks/useDatabase"; 

export default function Order() {
  const [modalVisible, setModalVisible] = useState(false);
  const [selectedOrder, setSelectedOrder] = useState<any>(null);
  const [reloadData, setReloadData] = useState(false); // Thêm state để trigger reload
  const { data: orders, loading, updateOrderStatus } = useFirebaseDatabase("Orders");
  const user = useSelector((state: any) => state.user);
  
  const employeeOrder = orders.filter(
    (ord) => ord.ord_status === "Unsolved" && ord.idEmployee === user
  );

  useEffect(() => {
    if (reloadData) {
      // Nếu reloadData là true, lấy lại dữ liệu
      setReloadData(false); // Reset lại state reloadData sau khi lấy xong
    }
  }, [reloadData]);

  const handleOrderComplete = async (orderId: string) => {
    try {
      await updateOrderStatus(orderId, "Completed"); // Cập nhật trạng thái "Completed"
      setReloadData(true); // Set lại reloadData để trigger reload dữ liệu
      Alert.alert("Order completed successfully");
    } catch (error) {
      console.error("Error completing order: ", error);
      Alert.alert("Failed to complete the order");
    }
  };

  return (
    <ScrollView showsVerticalScrollIndicator={false} style={styles.scrollView}>
      <View style={styles.headerContainer}>
        <Text style={styles.headerText}>Order</Text>
      </View>

      {loading ? (
        <View style={styles.loadingContainer}>
          <ActivityIndicator size="large" color="#fff" />
        </View>
      ) : (
        employeeOrder.map((order) => (
          <ListCard
            key={order._id}
            onPress={() => {
              setSelectedOrder(order);
              setModalVisible(true);
            }}
            order={order}
          />
        ))
      )}

      <View>
        <Modal
          animationType="slide"
          transparent={true}
          visible={modalVisible}
          onRequestClose={() => {
            Alert.alert("Modal is closed");
          }}
        >
          <ModalCard
            onPress={() => {
              setModalVisible(false);
              handleOrderComplete(selectedOrder._id); // Gọi hàm hoàn thành khi nhấn "Hoàn thành"
            }}
            order={selectedOrder}
          />
        </Modal>
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  scrollView: {
    backgroundColor: "#1e2e44",
  },
  headerContainer: {
    alignItems: "center",
    marginTop: 20,
  },
  headerText: {
    fontSize: 24,
    fontFamily: "RobotoBold", // Đảm bảo font này có sẵn
    color: "#FFFFFF",
    textAlign: "center",
  },
  loadingContainer: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    marginTop: 50,
  },
});
