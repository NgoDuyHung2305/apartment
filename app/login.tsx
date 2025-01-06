import {
  Image,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
  Alert,
  ActivityIndicator,
} from "react-native";
import React, { useState } from "react";
import { Provider, useDispatch, useSelector } from 'react-redux';  // Nhập Provider từ react-redux
import Ionicons from "react-native-vector-icons/Ionicons";
import SimpleLineIcons from "react-native-vector-icons/SimpleLineIcons";
import { router } from "expo-router";

import { useFirebaseDatabase } from "./hooks/useDatabase";
export default function Login() {
  const dispatch = useDispatch();  
  const { data: employees, loading: loadingEmployees } =
    useFirebaseDatabase("Employees");
  const [secureEntry, setSecureEntry] = useState(true);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [loading, setLoading] = useState(false);

  // Hàm kiểm tra đăng nhập
  const handleLogin = async () => {
    if (!email || !password) {
      alert("Please enter both email and password.");
      return;
    }

    setLoading(true);

    try {
      // Kiểm tra email và password
      const employee = employees.find(
        (emp) =>
          emp.empl_mail.toLowerCase() === email.toLowerCase() &&
          emp.empl_password === password
      );
      if (employee) {
        dispatch({ type: 'UPDATE_USER', payload: employee.id });
        router.push("/(tabs)");
      } else {
        Alert.alert("Login Failed");
      }
    } catch (error) {
      alert("Something went wrong. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  if (loadingEmployees) {
    return (
      <View style={styles.loadingContainer}>
        <ActivityIndicator size="large" color="#fff" />
        <Text style={styles.loadingText}>Loading employees...</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <View style={styles.iconContainer}>
        <Image
          source={require("../assets/images/icon1.png")}
          style={styles.icon}
        />
      </View>
      {/* Form */}
      <View style={styles.formContainer}>
        <View style={styles.inputContainer}>
          <Ionicons name={"mail-outline"} size={30} color="white" />
          <TextInput
            style={styles.textInput}
            placeholder="Enter your email"
            placeholderTextColor="white"
            keyboardType="email-address"
            value={email}
            onChangeText={setEmail}
          />
        </View>
        <View style={styles.inputContainer}>
          <SimpleLineIcons name={"lock"} size={30} color="white" />
          <TextInput
            style={styles.textInput}
            placeholder="Enter your password"
            placeholderTextColor="white"
            secureTextEntry={secureEntry}
            value={password}
            onChangeText={setPassword}
          />
          <TouchableOpacity
            onPress={() => {
              setSecureEntry((prev) => !prev);
            }}
          >
            <SimpleLineIcons name={"eye"} size={20} color="white" />
          </TouchableOpacity>
        </View>

        <TouchableOpacity
          style={styles.loginButtonWrapper}
          onPress={handleLogin}
          disabled={loading}
        >
          {loading ? (
            <ActivityIndicator size="small" color="#fff" />
          ) : (
            <Text style={styles.loginText}>Login</Text>
          )}
        </TouchableOpacity>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: "#1e2e44",
    padding: 20,
  },
  iconContainer: {
    alignItems: "center",
    marginVertical: 20,
  },
  formContainer: {
    marginTop: 20,
  },
  inputContainer: {
    borderWidth: 1,
    borderColor: "white",
    borderRadius: 100,
    paddingHorizontal: 20,
    flexDirection: "row",
    alignItems: "center",
    padding: 2,
    marginVertical: 10,
  },
  textInput: {
    flex: 1,
    paddingHorizontal: 10,
    fontFamily: "Bold",
    color: "white",
  },
  loginButtonWrapper: {
    backgroundColor: "#7369ff",
    borderRadius: 100,
    marginTop: 20,
  },
  loginText: {
    color: "white",
    fontSize: 20,
    fontFamily: "Bold",
    textAlign: "center",
    padding: 10,
  },
  icon: {
    width: 400,
    height: 400,
  },
  loadingContainer: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "#1e2e44",
  },
  loadingText: {
    color: "white",
    marginTop: 10,
    fontSize: 16,
  },
});
