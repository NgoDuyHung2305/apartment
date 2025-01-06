import React from 'react';
import { View, Text, TouchableHighlight, GestureResponderEvent, StyleSheet } from 'react-native';
import { useFirebaseDatabase } from '../hooks/useDatabase';

interface OrderCardProps {
  onPress: () => void;
  order: any; // Nhận dữ liệu order từ prop
}

const OrderCard: React.FC<OrderCardProps> = ({ onPress, order }) => {
  const { updateOrderStatus } = useFirebaseDatabase("Orders"); // Sử dụng hàm từ hook

  const handleCompleteOrder = async () => {
    try {
      await updateOrderStatus(order.id, "Solved");
      onPress();
    } catch (error) {
      console.error("Error completing order: ", error);
      alert("Failed to complete the order");
    }
  };
  return (
    <View style={styles.cardContainer}>
     <View style={styles.view}>
             <Text style={styles.text}>Phòng </Text>
             <Text style={styles.text}>{order.idRoom}</Text>
           </View>
     
           <View style={styles.view}>
             <Text style={styles.text}>Mô tả: </Text>
             <Text style={styles.text}>{order.ord_description}</Text>
           </View>
     
           <View style={styles.view}>
             <Text style={styles.text}>Ngày tạo: </Text>
             <Text style={styles.text}>{order.create_at}</Text>
           </View>

      <TouchableHighlight
        underlayColor="#6600bb"
        style={styles.updatebutton}
        onPress={handleCompleteOrder}
      >
        <Text style={styles.buttonText}>Hoàn thành</Text>
      </TouchableHighlight>

      <TouchableHighlight
        underlayColor="#6600bb"
        style={styles.cancelbutton}
        onPress={onPress}
      >
        <Text style={styles.buttonText}>Hủy</Text>
      </TouchableHighlight>
    </View>
  );
};

const styles = StyleSheet.create({
  cardContainer: {
    paddingHorizontal: 32,
    alignSelf: 'center',
    marginTop: 290,
    backgroundColor: '#FFF',
    height: 386,
    elevation: 1,
    width: 270,
    borderRadius: 15,
    borderWidth: 5,
    borderColor:'#4b3ca7'
  },
  view: {
    flexDirection: 'row',
    paddingTop: 20,
    alignItems: 'center',
  },
  text: {
    fontFamily: 'Bold',
    color: '#4b3ca7',
    fontSize: 20,
  },
 
  updatebutton: {
    width: 200,
    marginLeft: 5,
    elevation: 2,
    marginTop: 25,
    backgroundColor: '#44FEA1',
    paddingVertical: 13,
    borderRadius: 25,
    alignSelf: 'center',
  },
  cancelbutton: {
    width: 200,
    marginLeft: 5,
    elevation: 2,
    marginTop: 25,
    backgroundColor: '#F5A5A5',
    paddingVertical: 13,
    borderRadius: 25,
    alignSelf: 'center',
  },
  buttonText: {
    fontFamily: 'Bold',
    color: '#FFF',
    textAlign: 'center',
    fontSize: 18,
  },
});

export default OrderCard;
