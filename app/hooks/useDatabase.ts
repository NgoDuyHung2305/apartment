import { useEffect, useState } from 'react';
import { database } from '../firebaseConfig';
import { ref, onValue, update } from 'firebase/database';

export const useFirebaseDatabase = (path: string) => {
  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);

  // Lấy dữ liệu từ Firebase
  useEffect(() => {
    const dbRef = ref(database, path);

    const unsubscribe = onValue(dbRef, (snapshot) => {
      const value = snapshot.val();
      const list = value ? Object.keys(value).map((key) => ({ id: key, ...value[key] })) : [];
      setData(list);
      setLoading(false);
    });

    return () => unsubscribe();
  }, [path]);

  // Hàm cập nhật trạng thái của đơn hàng
  const updateOrderStatus = async (orderId: string, newStatus: string) => {
    try {
      const orderRef = ref(database, `Orders/${orderId}`);
      await update(orderRef, {
        ord_status: newStatus,
      });
      console.log("Order status updated successfully");
    } catch (error) {
      console.error("Error updating order status: ", error);
    }
  };

  return { data, loading, updateOrderStatus };
};
