import React, { useState } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, TextInput, ScrollView } from 'react-native';
import Icon from 'react-native-vector-icons/FontAwesome'; // Sử dụng icon
import { useSelector } from 'react-redux';
import { useFirebaseDatabase } from '../hooks/useDatabase';
import { database } from '../firebaseConfig';
import { ref, update } from 'firebase/database';

const EmployeeInfo = () => {
  const [isEditing, setIsEditing] = useState({
    empl_name: false,
    empl_phone: false,
    empl_email: false, // empl_email không thể chỉnh sửa
    empl_date_start: false,
    empl_password: false,
  });

  const [formData, setFormData] = useState({
    empl_name: '',
    empl_phone: '',
    empl_email: '',
    empl_date_start: '',
    empl_password: '',
  });

  const user = useSelector((state: any) => state.user); // Lấy thông tin người dùng hiện tại
  const { data: employees, loading: loadingEmployees } = useFirebaseDatabase('Employees');
  const employee = employees.find((emp) => emp.id === user);

  const [isAnyFieldEdited, setIsAnyFieldEdited] = useState(false);

  React.useEffect(() => {
    if (employee) {
      setFormData({
        empl_name: employee.empl_name || '',
        empl_phone: employee.empl_phone || '',
        empl_email: employee.empl_mail || '',
        empl_date_start: employee.empl_date_start || '',
        empl_password: employee.empl_password || '',
      });
    }
  }, [employee]);

  const handleEdit = (field: string) => {
    if (field !== 'empl_email') {
      setIsEditing((prev) => {
        const updated = { ...prev, [field]: true };
        setIsAnyFieldEdited(true);
        return updated;
      });
    }
  };

  const handleChange = (field: string, value: string) => {
    setFormData((prev) => ({
      ...prev,
      [field]: value,
    }));
  };

  const handleSave = async () => {
    if (employee) {
      const employeeRef = ref(database, `Employees/${employee.id}`);
      try {
        await update(employeeRef, formData);
        console.log('Employee data updated successfully');
      } catch (error) {
        console.error('Error updating employee data: ', error);
      }
    }

    setIsEditing({
      empl_name: false,
      empl_phone: false,
      empl_email: false,
      empl_date_start: false,
      empl_password: false,
    });
    setIsAnyFieldEdited(false);
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <Text style={styles.title}>Thông tin nhân viên</Text>

      <View style={styles.infoContainer}>
        <Text style={styles.label}>Tên:</Text>
        {isEditing.empl_name ? (
          <TextInput
            style={styles.input}
            value={formData.empl_name}
            onChangeText={(value) => handleChange('empl_name', value)}
            placeholder="Nhập tên"
            placeholderTextColor="#fff"
          />
        ) : (
          <Text style={styles.value}>{formData.empl_name || 'Chưa có thông tin'}</Text>
        )}
        <TouchableOpacity onPress={() => handleEdit('empl_name')}>
          <Icon name="pencil" size={20} color="#4b3ca7" style={styles.icon} />
        </TouchableOpacity>
      </View>


      <View style={styles.infoContainer}>
        <Text style={styles.label}>Số điện thoại:</Text>
        {isEditing.empl_phone ? (
          <TextInput
            style={styles.input}
            value={formData.empl_phone}
            onChangeText={(value) => handleChange('empl_phone', value)}
            placeholder="Nhập số điện thoại"
            placeholderTextColor="#fff"
          />
        ) : (
          <Text style={styles.value}>{formData.empl_phone || 'Chưa có thông tin'}</Text>
        )}
        <TouchableOpacity onPress={() => handleEdit('empl_phone')}>
          <Icon name="pencil" size={20} color="#4b3ca7" style={styles.icon} />
        </TouchableOpacity>
      </View>

      <View style={styles.infoContainer}>
        <Text style={styles.label}>empl_email:</Text>
        <Text style={styles.value}>{formData.empl_email || 'Chưa có thông tin'}</Text>
      </View>

      <View style={styles.infoContainer}>
        <Text style={styles.label}>Ngày vào làm:</Text>
        {isEditing.empl_date_start ? (
          <TextInput
            style={styles.input}
            value={formData.empl_date_start}
            onChangeText={(value) => handleChange('empl_date_start', value)}
            placeholder="Nhập ngày vào làm"
            placeholderTextColor="#fff"
          />
        ) : (
          <Text style={styles.value}>{formData.empl_date_start || 'Chưa có thông tin'}</Text>
        )}
      </View>

      <View style={styles.infoContainer}>
        <Text style={styles.label}>Mật khẩu:</Text>
        {isEditing.empl_password ? (
          <TextInput
            style={styles.input}
            value={formData.empl_password}
            onChangeText={(value) => handleChange('empl_password', value)}
            placeholder="Nhập mật khẩu"
            placeholderTextColor="#fff"
          />
        ) : (
          <Text style={styles.value}>{formData.empl_password || 'Chưa có thông tin'}</Text>
        )}
        <TouchableOpacity onPress={() => handleEdit('empl_password')}>
          <Icon name="pencil" size={20} color="#4b3ca7" style={styles.icon} />
        </TouchableOpacity>
      </View>

      {isAnyFieldEdited && (
        <TouchableOpacity style={styles.saveButton} onPress={handleSave}>
          <Text style={styles.saveButtonText}>Lưu thông tin</Text>
        </TouchableOpacity>
      )}
    </ScrollView>
  );
};
const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#1e2e44',
    paddingBottom: 30,
  },
  title: {
    fontSize: 26,
    marginTop: 10,
    fontWeight: 'bold',
    marginBottom: 50,
    textAlign: 'center',
    color: '#fff',
  },
  infoContainer: {
    flexDirection: 'row',
    marginBottom: 50,
    marginLeft: 20,
    alignItems: 'center',
    justifyContent: 'space-between',
  },
  label: {
    fontSize: 20,
    fontWeight: 'bold',
    color: '#fff',
    width: 150,
  },
  value: {
    fontSize: 20,
    color: '#fff',
    flex: 1,
  },
  input: {
    fontSize: 20,
    color: '#fff',
    flex: 1,
    borderBottomWidth: 1,
    borderColor: '#ddd',
    padding: 10,
    marginRight: 10
  },
  icon: {
    marginLeft: 20,
  },
  saveButton: {
    marginTop: 20,
    backgroundColor: '#44FEA1',
    paddingVertical: 18,
    borderRadius: 25,
    alignSelf: 'center',
    width: '80%',
  },
  saveButtonText: {
    fontFamily: 'RobotoBold',
    color: '#FFF',
    textAlign: 'center',
    fontSize: 22,
  },
});

export default EmployeeInfo;
