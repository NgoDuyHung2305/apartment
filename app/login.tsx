import {
  Image,
  StyleSheet,
  Text,
  TextInput,
  TouchableOpacity,
  View,
} from "react-native";
import React, { useState, createContext, useContext } from "react";
import Ionicons from "react-native-vector-icons/Ionicons";
import SimpleLineIcons from "react-native-vector-icons/SimpleLineIcons";
import { useRouter } from "expo-router";
import { ref, get } from 'firebase/database';
import { database } from "./firebaseConfig";

export let recentemail = 'abc';

export const UserContext= createContext<any>(null);

export default function LoginScreen() {
  const [secureEntery, setSecureEntery] = useState(true);
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState<string | null>(null);

  const setUserContext = useContext(UserContext);
  const router = useRouter();

  const handleLogin = async () => {
    setError(null);  // Reset error trước mỗi lần login

    if (!email || !password) {
      setError("Please enter both email and password.");
      return;
    }

    try {
      const snapshot = await get(ref(database, 'Dwellers'));
      if (snapshot.exists()) {
        const dwellers = snapshot.val();
        const matchedDweller = Object.values(dwellers).find(
          (dweller: any) => dweller.dweller_email === email && dweller.password === password
        );
  
        if (matchedDweller) {
          //setUserContext(matchedDweller);
          recentemail = email;
          router.replace('/(tabs)');
        } else {
          setError("Invalid email or password.");
        }
      } else {
        setError("No users found.");
      }
    } catch (error) {
      setError("Failed to connect to database.");
      console.error(error);
    }
  };

  return (
    <View style={styles.container}>
      <View style={styles.iconContainer}>
        <Image
          source={require("../assets/images/Double H CIty icon.png")}
          style={styles.icon}
        />
      </View>

      {/* form  */}
      <View style={styles.formContainer}>
        {error && <Text style={styles.errorText}>{error}</Text>}  {/* Hiển thị lỗi ở đây */}
        
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
            secureTextEntry={secureEntery}
            value={password}
            onChangeText={setPassword}
          />
          <TouchableOpacity
            onPress={() => setSecureEntery((prev) => !prev)}
          >
            <SimpleLineIcons name={secureEntery ? "eye" : "eye-off"} size={20} color="white" />
          </TouchableOpacity>
        </View>

        <TouchableOpacity
          style={styles.loginButtonWrapper}
          onPress={handleLogin}
        >
          <Text style={styles.loginText}>Login</Text>
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
  errorText: {
    color: 'red',
    textAlign: 'center',
    marginBottom: 10,
    fontSize: 16,
  },
});
