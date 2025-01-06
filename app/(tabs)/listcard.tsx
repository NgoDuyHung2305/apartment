import React from "react";
import { View, Text, TouchableOpacity, StyleSheet } from "react-native";

interface ListCardProps {
  onPress: () => void;
  order: any; // Chứa dữ liệu order
}

const ListCard: React.FC<ListCardProps> = ({ onPress, order }) => {
  return (
    <TouchableOpacity onPress={onPress} style={styles.cardContainer}>

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
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  cardContainer: {
    paddingHorizontal: 32,
    alignSelf: "center",
    marginTop: 20,
    backgroundColor: "#FFF",
    height: 182,
    elevation: 1,
    width: 270,
    borderRadius: 16,
  },
  view: {
    flexDirection: "row",
    paddingTop: 20,
    alignSelf: "center",
  },
  text: {
    fontFamily: "Bold",
    color: "#4b3ca7",
    fontSize: 20,
  },
});

export default ListCard;
