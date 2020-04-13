$(function () {
    $("input").checkboxradio({
        icon: false
    });

    // createScheduler(ganttData,treeGroup);
    //createGantt(ganttData, ownerNames);
    // CreateDieZhuang()
  //  modSampleHeight();
    $("#QiShenHere").hide();
});
function showDiezhuang() {
    $("#QiShenHere").hide();
    $("#DieZhuangHere").show();
    $("#Rulu_Here").hide();
}
function showQiShen() {

    $("#DieZhuangHere").hide();
    $("#QiShenHere").show();
    $("#Rulu_Here").hide();

    scheduler.update_view();
}
function showRuLu() {

    $("#DieZhuangHere").hide();
    $("#QiShenHere").hide();
    $("#Rulu_Here").show();

}

function modSampleHeight() {
    var headHeight = 100;
    var sch = document.getElementById("DieZhuangHere");
    sch.style.height = (parseInt(document.body.offsetHeight) - headHeight) + "px";
   // DieZhuangHere.setSizes();
}