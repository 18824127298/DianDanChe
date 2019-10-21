
import '../css/common.css';

const requireContext = require.context("../images", true, /^\.\/.*$/);
const images = requireContext.keys().map(requireContext);

export default function(){
	this.init = function(){
		let htmlCont = `
			<section class="security">
				<div class="wrap">
					<h3>项目<i>安全保障</i></h3>
					<div class="security_child flex">
						<div class="sec_prochild">
							<div class="sec_prochild_img flex">
								<img src="${images[0]}" alt="">
							</div>
							<div class="sec_prochild_text">
								<p><i>真实车辆抵/质押</i></p>
								借款人即车主，所有抵押借款均办理抵押登记，所有质押车辆停放专属车库，保障产品真实可靠。
							</div>
						</div>
						<div class="sec_prochild">
							<div class="sec_prochild_img flex">
								<img src="${images[1]}" alt="">
							</div>
							<div class="sec_prochild_text">
								<p><i>小额分散更安全</i></p>
								平台以小额借款项目为主，单笔借款不超过20万元，确保投资分散低风险，增强抗风险能力。
							</div>
						</div>
						<div class="sec_prochild">
							<div class="sec_prochild_img flex">
								<img src="${images[2]}" alt="">
							</div>
							<div class="sec_prochild_text">
								<p><i>项目公开透明</i></p>
								所有借款项目手续资料齐全，除涉及借款人隐私，均会在网站上予以公示，投资人可随时查看验证。
							</div>
						</div>
					</div>
				</div>
			</section>
			<section class="security funds">
				<h3 class="while">资金<i>安全保障</i></h3>
				<div class="wrap">
					<div class="saveurity_child">
						<img src="${images[4]}" alt="">
						<div class="save_prochild">
							<div class="save_prochild_text">
								<p class="title"><i>交易过程安全有保障</i></p>
								<p class="cont">
									丰利金服投资支付方式采取第三方支付机构支付，资金交易过程，交易记录，交易信息，安全无泄露。实名绑定银行卡，充值、提现实行同卡进出原则，确保资金万无一失。
								</p>
							</div>
						</div>
					</div>
				</div>
			</section>
			<section class="security wind_control">
				<h3 class="while">风控<i>安全保障</i></h3>
				<div class="wrap">
					<div class="wind_child flex">
						<div class="wind_prochild">
							<div class="wind_prochild_img flex">
								<img src="${images[3]}" alt="">
							</div>
							<div class="wind_prochild_text">
								<p>银行级别贷前审查</p>
								<div class="wind_prochilds_text">
									贷前会与借款人进行面谈，了解借款人的借款用途、性格特点，根据借款人出具的一系列资料审核借款人资质，明确借款人借款用途、还款来源、负债情况、名下资产情况等，并上门进行实地考察。
								</div>
							</div>
						</div>
						<div class="wind_prochild">
							<div class="wind_prochild_img flex">
								<img src="${images[1]}" alt="">
							</div>
							<div class="wind_prochild_text">
								<p>小额分散合规资产风险可控</p>
								<div class="wind_prochilds_text">
									资产端以小额借款项目为主，出借人可分散投资给多个借款债权人，极大地降低风险，出借更安心。
								</div>
							</div>
						</div>
						<div class="wind_prochild">
							<div class="wind_prochild_img flex">
								<img src="${images[5]}" alt="">
							</div>
							<div class="wind_prochild_text">
								<p>贷后管理逾期催收</p>
								<div class="wind_prochilds_text">
									根据客服及业务员提供的信息，对借款人进行家访调查，审查是否属实，提醒借款人还款日期及金额。若借款人逾期未能还款，则进行电话及上门催收工作。
								</div>
							</div>
						</div>
					</div>
				</div>
			</section>
			<section class="security skill">
				<h3 class="while">技术<i>安全保障</i></h3>
				<div class="wrap">
					<div class="skill_child flex">
						<div class="skill_prochild_text">
							<p>网站安全</p>
							资深的IT技术服务团队：团队包括全球顶尖对冲基金的技术专家和资深网站工程师，网站100%自主研发，充分考虑网站安全。
							<br /><br />
							完善的软件平台和监测系统：数据库每日备份，所有用户操作均有记录，确保在系统发生异常时能及时进行数据恢复，保障数据的完整。
						</div>
						<div class="skill_prochild_text">
							<p>数据安全</p>
							网站安全系统自动扫描系统状况，随时检测黑客入侵记录和数据错误，一旦系统出现风险自动进入保护状态并通知技术专家及时解决，确保用户数据安全。
							<br /><br />
							采用国际权威VeriSign SSL 256位数字加密技术对用户传输数据进行加密。
						</div>
						<div class="skill_prochild_text">
							<p>隐私安全</p>
							多层防火墙隔离外部网络，服务器和数据库，即使黑客成功入侵网站服务器也无法直接连入用户数据库，有效防止任何人包括公司员工取得用户信息。
							<br /><br />
							网站不保存用户密码和密码保护问题答案，用MD5等不可逆算法生成的加密信息验证用户。丰利金服在任何情况下都不会以任何形式泄漏您的信息。
						</div>
					</div>
				</div>
			</section>
			<section class="register">
					<a href=""><button id="regist">立即注册</button></a>
			</section>`;
		let bodys = document.querySelector("body");
		bodys.innerHTML += htmlCont;
	}
}