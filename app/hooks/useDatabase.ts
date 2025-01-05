import { useEffect, useState } from 'react';
import { database } from '../firebaseConfig';
import { ref, onValue } from 'firebase/database';

export const useFirebaseDatabase = (path: string) => {
  const [data, setData] = useState<any[]>([]);
  const [loading, setLoading] = useState(true);

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

  return { data, loading };
};
