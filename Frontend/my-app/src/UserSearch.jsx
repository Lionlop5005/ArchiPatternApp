import React, { useState } from "react";
import axios from "axios";

const UserSearch = () => {
  const [name, setName] = useState("");
  const [results, setResults] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const handleSearch = async (e) => {
    e.preventDefault();

    if (!name.trim()) {
      setError("名前を入力してください");
      return;
    }

    setLoading(true);
    setError("");
    setResults([]);

    try {
      const response = await axios.get("https://localhost:57678/api/users/search", {
        params: { name }
      });
      console.log("APIレスポンス:", response.data);

      if (Array.isArray(response.data)) {
        setResults(response.data);
      } else {
        setError("検索結果が配列ではありません");
      }
    } catch (err) {
      console.error(err);
      setError(err.response?.data?.message || "検索に失敗しました");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={{ padding: "20px", maxWidth: "700px", margin: "0 auto", fontFamily: "sans-serif" }}>
      <h2>ユーザー検索</h2>
      <form onSubmit={handleSearch} style={{ marginBottom: "15px" }}>
        <input
          type="text"
          placeholder="名前を入力"
          value={name}
          onChange={(e) => setName(e.target.value)}
          style={{ padding: "8px", width: "70%", marginRight: "10px" }}
        />
        <button type="submit" style={{ padding: "8px 16px" }} disabled={loading}>
          {loading ? "検索中..." : "検索"}
        </button>
      </form>

      {error && <p style={{ color: "red" }}>{error}</p>}

      {results.length > 0 ? (
        <table style={{ width: "100%", borderCollapse: "collapse", marginTop: "10px" }}>
          <thead>
            <tr>
              <th style={{ border: "1px solid #ccc", padding: "8px" }}>ID</th>
              <th style={{ border: "1px solid #ccc", padding: "8px" }}>名前</th>
              <th style={{ border: "1px solid #ccc", padding: "8px" }}>メール</th>
              <th style={{ border: "1px solid #ccc", padding: "8px" }}>作成日</th>
            </tr>
          </thead>
          <tbody>
            {results.map((user) => (
              <tr key={user.id}>
                <td style={{ border: "1px solid #ccc", padding: "8px" }}>{user.id}</td>
                <td style={{ border: "1px solid #ccc", padding: "8px" }}>{user.name}</td>
                <td style={{ border: "1px solid #ccc", padding: "8px" }}>{user.email}</td>
                <td style={{ border: "1px solid #ccc", padding: "8px" }}>{new Date(user.createdAt).toLocaleString()}</td>
              </tr>
            ))}
          </tbody>
        </table>
      ) : (
        !loading && <p>検索結果はありません</p>
      )}
    </div>
  );
};

export default UserSearch;
