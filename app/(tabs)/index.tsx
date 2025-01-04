import { Image, StyleSheet, View, Text, TouchableOpacity, TextInput } from 'react-native';
import React, { useState } from 'react';

export default function HomeScreen() {
  const [isEditing, setIsEditing] = useState(false);

  const [user, setUser] = useState({
    name: 'Hưng Phan',
    email: '22520523@gm.uit.edu.com',
    birthday: '31/03/2004',
    hometown: 'Kon Tum',
    phone: '0393191574',
    sex: 'Male',
    room: 'A01',
    avatar: 'https://cdnphoto.dantri.com.vn/a3moJbXIWlIKrgZtq5Ffhtj-V7U=/thumb_w/1020/2024/11/22/messi-crop-1732270819764.jpeg', // Tạo avatar ngẫu nhiên
  });

  const [editableUser, setEditableUser] = useState({ ...user });

  const handleSave = () => {
    setUser(editableUser);
    setIsEditing(false);
  };

  const handleEdit = () => {
    setEditableUser({ ...user });
    setIsEditing(true);
  };

  return (
    <View style={styles.container}>
      <View style={styles.profileContainer}>
        <Image source={{ uri: user.avatar }} style={styles.avatar} />
        <Text style={styles.name}>{user.name}</Text>
      </View>

      <View style={styles.infoContainer}>
        {Object.entries(editableUser).map(([key, value]) => {
          if (key === 'avatar' || key === 'name') return null; // Bỏ qua avatar và name
          return (
            <View style={styles.infoRow} key={key}>
              <Text style={styles.label}>{key.charAt(0).toUpperCase() + key.slice(1)}:</Text>
              {isEditing ? (
                <TextInput
                  style={styles.input}
                  value={String(value)} // Đảm bảo giá trị là chuỗi
                  onChangeText={(text) => setEditableUser({ ...editableUser, [key]: text })}
                />
              ) : (
                <Text style={styles.value}>{value}</Text>
              )}
            </View>
          );
        })}
      </View>

      <TouchableOpacity
        style={styles.editButton}
        onPress={isEditing ? handleSave : handleEdit}
      >
        <Text style={styles.editText}>{isEditing ? 'Save' : 'Edit Profile'}</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#f4f4f4',
    padding: 20,
  },
  profileContainer: {
    alignItems: 'center',
    marginBottom: 30,
  },
  avatar: {
    width: 120,
    height: 120,
    borderRadius: 60,
    marginBottom: 10,
  },
  name: {
    fontSize: 26,
    fontWeight: 'bold',
    color: '#333',
  },
  infoContainer: {
    width: '100%',
    backgroundColor: 'white',
    borderRadius: 10,
    padding: 20,
    shadowColor: '#000',
    shadowOffset: { width: 0, height: 2 },
    shadowOpacity: 0.1,
    shadowRadius: 4,
    elevation: 3,
  },
  infoRow: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    marginVertical: 10,
  },
  label: {
    fontSize: 16,
    fontWeight: '500',
    color: '#555',
    flex: 1,
  },
  value: {
    fontSize: 16,
    color: '#333',
    flex: 2,
    textAlign: 'right',
  },
  input: {
    flex: 2,
    borderBottomWidth: 1,
    borderBottomColor: '#ccc',
    fontSize: 16,
    textAlign: 'right',
  },
  editButton: {
    marginTop: 30,
    backgroundColor: '#7369ff',
    paddingVertical: 12,
    paddingHorizontal: 50,
    borderRadius: 30,
  },
  editText: {
    color: 'white',
    fontSize: 18,
    fontWeight: '600',
  },
});
