
var order_sheet = {}
order_sheet["summa"]=0
function add_Goods(id)
{   if (order_sheet[id] == undefined) {
        order_sheet[id] = {
            "count":1,
            "price": Goods[id]["price"],
            "name": Goods[id]["name"]
        }


        document.getElementById("table_order").innerHTML += "<tr>" 
                            +"<td>"+Goods[id]["name"]+"</td>" 
                            + "<td>" + Goods[id]["price"] + "₽</td>" 
                            +"<td id=\"goods_count"+id+"\">1</td>" 
                            +"</tr>"

    } else {
        order_sheet[id]["count"] += 1;
        document.getElementById("goods_count" + id).innerHTML = order_sheet[id]["count"];
       
      
    }
    //console.log(order_sheet)
    /*order_sheet["summa"] = order_sheet[id]["price"]
    document.getElementById("summa").innerHTML = order_sheet["summa"]*/
    var total_sum = 0;
    for (var item_id in order_sheet) {
        if (item_id !== "summa") {
            total_sum += order_sheet[item_id]["price"] * order_sheet[item_id]["count"];
        }
    }
    order_sheet["summa"] = total_sum;
    document.getElementById("summa").innerHTML = order_sheet["summa"] + " ₽"

}

/*async function Buy_goods() {
    let send_order_sheet = {};
    order_sheet.forEach((item) => {
        send_order_sheet[item["id"]] = item["count"];
    });

    let response = await fetch('./Home/Index', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(send_order_sheet)
    });
}*/


async function Buy_Goods() {
    let send_order_sheet = {};
    for (var key in order_sheet) {
        send_order_sheet[key] = order_sheet[key]["count"];
    }

    let response = await fetch('/Home/Index', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(send_order_sheet)
    });

    document.getElementById("table_order").innerHTML = "<tr><td>Name</td><td>Price</td><td>Count</td></tr>"
    order_sheet = {}
    const messageElement = document.createElement('div');
    messageElement.className = 'success-message';
    messageElement.textContent = 'Заказ отправлен';
    document.body.appendChild(messageElement);
}
