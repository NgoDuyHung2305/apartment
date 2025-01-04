import {
    Image,
    StyleSheet,
    Text,
    TextInput,
    TouchableOpacity,
    View,
  } from "react-native";
  import React, { useState } from "react";
  
  import Ionicons from "react-native-vector-icons/Ionicons";
  import SimpleLineIcons from "react-native-vector-icons/SimpleLineIcons";
  import { useRouter } from "expo-router";
  
  export default function LoginScreen() {
    const [secureEntery, setSecureEntery] = useState(true);
    const router = useRouter();

    const handleLogin = () => {
      router.replace('/(tabs)');
    }
  
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
          <View style={styles.inputContainer}>
            <Ionicons name={"mail-outline"} size={30} color="white" />
            <TextInput
              style={styles.textInput}
              placeholder="Enter your email"
              placeholderTextColor="white"
              keyboardType="email-address"
            />
          </View>
          <View style={styles.inputContainer}>
            <SimpleLineIcons name={"lock"} size={30} color="white" />
            <TextInput
              style={styles.textInput}
              placeholder="Enter your password"
              placeholderTextColor="white"
              secureTextEntry={secureEntery}
            />
          <TouchableOpacity
            onPress={() => {
              setSecureEntery((prev) => !prev);
            }}
          >
            <SimpleLineIcons name={secureEntery?"eye":"eye-off"} size={20} color="white" />
          </TouchableOpacity>
          </View>

          <TouchableOpacity style={styles.loginButtonWrapper} onPress={handleLogin}>
            <Text style={styles.loginText}>Login</Text>
          </TouchableOpacity>
        </View>
      </View>
    );
  };

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
      color: "white"
    },
    forgotPasswordText: {
      textAlign: "right",
      color: "white",
      fontFamily: "Bold",
      marginVertical: 10,
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
    icon:{
        width:400,
        height:400,
    }
  });

  