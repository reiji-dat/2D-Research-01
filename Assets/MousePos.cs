using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePos : MonoBehaviour
{
    [SerializeField]
    GameObject hit;

    [SerializeField]
    GameObject not;

    float camDist = 0;
    // Start is called before the first frame update
    void Start()
    {
        camDist = Vector3.Distance(Camera.main.transform.position, transform.position);

        //長さ0.1のマスで判定領域を表示
        for (float y = -5; y <= 5; y += 0.1f)
            for (float x = -5; x <= 5; x += 0.1f)
            {
                var pos = new Vector3(x, y, 0);
                ShikakuHantei(pos, 30);
            }
    }

    // Update is called once per frame
    void Update()
    {
        //マウスの位置を取る
        var mp = Input.mousePosition; //マウス座標を取る
        mp.z = camDist; //奥行をカメラの距離に変更
        var wp = Camera.main.ScreenToWorldPoint(mp); //スクリーン座標をワールド座標に変更
        //Debug.Log($"{wp}");
        transform.position = wp;

        var pos = wp;
        /*
        //1.正方形45度の当たり判定
        //x又はyに割った分だけ当たり判定がそれぞれの軸に広がる
        //左辺:対角線の長さが当たり判定の大きさ
        if (1 >= Mathf.Abs(pos.x) + Mathf.Abs(pos.y))
        {
            Debug.Log("当たった");
        }
        */

        /*
        //2.正方形の当たり判定
        //同じくxyを掛けると長方形にできる
        //同じくxyを足すと移動できる
        //左辺:一辺の長さが当たり判定
        if (1 >= Mathf.Abs(pos.x + pos.y) + Mathf.Abs(pos.x - pos.y))
        {
            Debug.Log("当たった");
        }
        */

        //少々複雑の為関数にした
        KaitenShikaku(pos, 30);
    }

    /// <summary>
    /// 判定領域の表示
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="theta"></param>
    void ShikakuHantei(Vector3 pos ,float theta)
    {
        float rad = -theta * Mathf.Deg2Rad;//Unityの仕様上-1を掛けている(別の環境によって変える)

        var up = Mathf.Cos(rad) * pos.x + (-Mathf.Sin(rad) * pos.y);
        var down = Mathf.Sin(rad) * pos.x + Mathf.Cos(rad) * pos.y;
        
        if (3 >= Mathf.Abs(up + down) + Mathf.Abs(up - down))
            Instantiate(hit, pos, Quaternion.identity);
        else
            Instantiate(not, pos, Quaternion.identity);
    }

    /// <summary>
    /// 斜めの矩型判定
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="theta"></param>
    void KaitenShikaku(Vector3 pos ,float theta)
    {
        //弧度法に変換
        float rad = -theta * Mathf.Deg2Rad;

        //回転行列を利用
        //参考:https://www.sist.ac.jp/~kanakubo/research/hosoku/kaiten_gyoretu.html
        //up:xのこと
        var up = Mathf.Cos(rad) * pos.x + (-Mathf.Sin(rad) * pos.y);
        //down:yのこと
        var down = Mathf.Sin(rad) * pos.x + Mathf.Cos(rad) * pos.y;

        Debug.Log($"{Mathf.Abs(up + down) + Mathf.Abs(up - down)}");

        //左辺:1辺の大きさ
        //右辺:
        if (3 >= Mathf.Abs(up + down) + Mathf.Abs(up - down))
            Debug.Log("当たった");   
    }
}
